import { Injectable } from '@angular/core';
import { Product } from '../models/product';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private productService:ProductService) { }

  public cartList:any[]= JSON.parse(localStorage.getItem('cartItems') || '[]');

  addToCart(product:any){
    this.cartList.push({...product, quantity:1});

    localStorage.setItem('cartItems', JSON.stringify(this.cartList))
  }


  getCart(){
    return this.cartList;
  }

  delete(product:any){
    this.cartList = this.cartList.filter((i)=>i.productId !== product.productId);

    localStorage.setItem('cartItems', JSON.stringify(this.cartList))
  }
    
  incrementQuantity(id:number){
    let product = this.cartList.find((i)=>i.productId === id)
    this.productService.GetProductById(product.productId).subscribe((data)=>{

      if(product.quantity<=data.stock-1){
        product.quantity++;
      }
      
    })
    

    localStorage.setItem('cartItems', JSON.stringify(this.cartList))
  }

  decrementQuantity(id:number){
    let product = this.cartList.find((i)=>i.productId === id)
    if(product.quantity!=1){
      product.quantity--;
    }

    localStorage.setItem('cartItems', JSON.stringify(this.cartList))
  }

  getTotal(){
    return this.cartList.reduce((acc,item)=>{
      return acc + item.price* item.quantity;
    },0)
  }

  clear(){
    this.cartList.forEach(element => {
      this.cartList.pop()
      this.cartList.pop()
    });
  }


  }
