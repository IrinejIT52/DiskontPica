import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { CommonModule } from '@angular/common';
import { CartService } from '../../../services/cart.service';


@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet,RouterLink,CommonModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {


  constructor(private productService:ProductService,private router:Router,private cartService:CartService){}

  logout(){
    localStorage.removeItem('token')
    localStorage.removeItem('cartItems')
    this.cartService.clear();
    this.router.navigateByUrl('/login')
  }

}
