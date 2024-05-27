import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
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
export class LayoutComponent implements OnInit,OnChanges {

  isLogged:boolean=false;

  constructor(private productService:ProductService,private router:Router,private cartService:CartService){}

  ngOnChanges(): void {
    this.isLoggedIn()
  }

  ngOnInit(): void {
    this.isLoggedIn()
  }


  login(){
    this.isLoggedIn()
    this.router.navigateByUrl('/login')
  }

  logout(){
    this.isLoggedIn()
    localStorage.removeItem('token')
    localStorage.removeItem('cartItems')
    this.cartService.clear();
    this.router.navigateByUrl('/login')
  }


  isLoggedIn(){
    if(localStorage.getItem('token')==null){
      this.isLogged=false
      
    }
    else{
      this.isLogged=true;
      
    }
  }

}
