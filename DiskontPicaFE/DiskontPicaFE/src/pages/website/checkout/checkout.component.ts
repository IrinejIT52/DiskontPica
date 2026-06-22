import { Component, OnInit, inject } from '@angular/core';
import { CommonModule, formatDate } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CartService } from '../../../services/cart.service';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Order } from '../../../models/order';
import { OrderItem } from '../../../models/orderItem';
import { CustomerService } from '../../../services/customer.service';
import { OrdersService } from '../../../services/orders.service';
import { PaymentService } from '../../../services/payment.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatButtonModule, MatInputModule, MatFormFieldModule],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent implements OnInit {
  checkoutForm!: FormGroup;
  cartService = inject(CartService);

  order: Order = new Order();
  date = new Date();
  orderId!: number;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private orderService: OrdersService,
    private paymentService: PaymentService
  ) {}

  ngOnInit(): void {
    this.checkoutForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      address: ['', Validators.required],
      city: ['', Validators.required],
      zip: ['', Validators.required]
    });
  }

  parseJwt(token: any) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(
      window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      }).join('')
    );
    return JSON.parse(jsonPayload);
  }

  onSubmit(): void {
    if (this.cartService.getCart().length === 0) {
      alert('Your cart is empty! Please add some items before checking out.');
      return;
    }

    if (this.checkoutForm.valid) {
      if (localStorage.getItem('token') == null) {
        return;
      }

      const { name } = this.parseJwt(localStorage.getItem('token'));
      this.customerService.GetCustomerByName(name).subscribe((data: any) => {
        this.order.customerId = data[0].customerId;
        
        const cartList = this.cartService.getCart();
        this.order.orderItems = [];

        cartList.forEach((product: any) => {
          const orderItem: OrderItem = {
            orderId: 0,
            orderItemId: 0,
            productId: product.productId,
            quantity: product.quantity,
            priceQuantity: 0
          };
          this.order.orderItems.push(orderItem);
        });

        const datee = formatDate(this.date, 'yyyy-MM-dd', 'en-US');
        this.order.orderDate = datee;
        this.order.orderStatus = 0; // PENDING
        this.order.orderType = 0;   // REGULAR
        
        // Save the shipping info in additionalInfo
        const formVals = this.checkoutForm.value;
        this.order.addiitionalInfo = `${formVals.firstName} ${formVals.lastName}, ${formVals.address}, ${formVals.city} ${formVals.zip}`;
        this.order.finalPrice = this.cartService.getTotal();

        this.orderService.AddOrder(this.order).subscribe({
          next: (data: any) => {
            this.cartService.clear();
            this.orderService.GetOrderByCustomer(this.order.customerId).subscribe({
              next: (ordersData: any) => {
                var lastIndex = ordersData.length - 1;
                this.orderId = ordersData[lastIndex].orderId;

                this.paymentService.CreateCheckOutSession(this.orderId).subscribe({
                  next: (url: any) => {
                    // Check if the url is wrapped in an object or has quotes
                    let redirectUrl = typeof url === 'string' ? url : (url.url || url.value || url);
                    // Remove quotes if they exist
                    redirectUrl = redirectUrl.replace(/"/g, '');
                    window.location.href = redirectUrl;
                  },
                  error: (err) => alert('Failed to create Stripe session: ' + err.message)
                });
              },
              error: (err) => alert('Failed to retrieve order: ' + err.message)
            });
          },
          error: (err) => alert('Failed to save order to database. Error: ' + JSON.stringify(err.error || err.message))
        });
      });
    } else {
      this.checkoutForm.markAllAsTouched();
    }
  }
}
