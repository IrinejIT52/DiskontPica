import { CommonModule, DatePipe, formatDate } from '@angular/common';
import { Component, Inject, inject } from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { PaymentService } from '../../../services/payment.service';
import { Order } from '../../../models/order';
import { CustomerService } from '../../../services/customer.service';
import { OrderItem } from '../../../models/orderItem';
import { OrdersService } from '../../../services/orders.service';
import { Router } from '@angular/router';
import { last, timeInterval } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-customer-cart',
  standalone: true,
  imports: [CommonModule,MatButtonModule],
  templateUrl: './customer-cart.component.html',
  styleUrl: './customer-cart.component.css'
})
export class CustomerCartComponent {


  parseJwt(token:any) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
  }


  order:Order = new Order();
  token!:string;
  cartList:any=[];
  orderItem!:OrderItem;
  date=new Date();
  orderId!:number;

  constructor(private router:Router,private paymentService:PaymentService,private customerService:CustomerService,private orderService:OrdersService){}


  cartService = inject(CartService)

  removeFromCart(product:any){
    this.cartService.delete(product);
  }


  checkOut(){

    if(localStorage.getItem('token')==null){
      this.router.navigateByUrl('/login')
      
    }
    else{
  
    //customerId
    const{name}= this.parseJwt(localStorage.getItem('token'))
    this.customerService.GetCustomerByName(name).subscribe((data)=>{
      this.order.customerId=data[0].customerId;
      
      this.cartList=this.cartService.getCart();

      this.order.orderItems=[];

      this.cartList.forEach((product:any) => {
        this.orderItem = {
          orderId:0,
          orderItemId:0,
          productId:product.productId,
          quantity:product.quantity,
          priceQuantity:0
        }
        this.order.orderItems.push(this.orderItem)
      });

       const datee=formatDate(this.date,'yyyy-MM-dd', 'en-US')
    
      this.order.orderDate= datee;
      this.order.orderStatus=0;
      this.order.orderType=0;
      this.order.addiitionalInfo="additional info";
      this.order.finalPrice=0;


      this.orderService.AddOrder(this.order).subscribe((data)=>{
        this.cartService.clear();
        this.orderService.GetOrderByCustomer(this.order.customerId).subscribe((data)=>{
          console.log(data)
          var lastIndex=data.length-1;
          this.orderId=data[lastIndex].orderId;
  
          this.paymentService.CreateCheckOutSession(this.orderId).subscribe((data)=>{
            document.location.href = data;
          })
        })
      });
      

     


      



    })
  }
  
  }

}
