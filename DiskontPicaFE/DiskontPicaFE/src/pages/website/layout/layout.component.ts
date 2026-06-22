import { Component, OnInit, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { CommonModule, formatDate } from '@angular/common';
import { CartService } from '../../../services/cart.service';
import { MatButtonModule } from '@angular/material/button';
import { CustomerService } from '../../../services/customer.service';
import { OrdersService } from '../../../services/orders.service';
import { PaymentService } from '../../../services/payment.service';
import { Order } from '../../../models/order';
import { OrderItem } from '../../../models/orderItem';


@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule, MatButtonModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent implements OnInit {

  isLogged: boolean = false;
  cartOpen: boolean = false;

  cartService = inject(CartService);

  order: Order = new Order();
  cartList: any = [];
  orderItem!: OrderItem;
  date = new Date();
  orderId!: number;

  constructor(
    private productService: ProductService,
    private router: Router,
    private customerService: CustomerService,
    private orderService: OrdersService,
    private paymentService: PaymentService
  ) {}

  ngOnInit(): void {
    this.isLoggedIn();
  }

  toggleCart() {
    this.cartOpen = !this.cartOpen;
  }

  login() {
    this.isLoggedIn();
    this.router.navigateByUrl('/login');
  }

  logout() {
    this.isLoggedIn();
    localStorage.removeItem('token');
    localStorage.removeItem('cartItems');
    this.cartService.clear();
    this.cartOpen = false;
    this.router.navigateByUrl('/login');
  }

  isLoggedIn() {
    if (localStorage.getItem('token') == null) {
      this.isLogged = false;
    } else {
      this.isLogged = true;
    }
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

  checkOut() {
    this.cartOpen = false;

    if (localStorage.getItem('token') == null) {
      this.router.navigateByUrl('/login');
      return;
    }

    this.router.navigateByUrl('/checkout');
  }
}
