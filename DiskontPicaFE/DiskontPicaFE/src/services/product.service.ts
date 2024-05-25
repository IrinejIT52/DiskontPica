import { HttpClient } from '@angular/common/http';
import { COMPILER_OPTIONS, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Constant } from './constants';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {


  constructor(private httpClient:HttpClient) { }

  public GetAllProducts():Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT)
  }

  public GetProductById(Id:number):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT+Id)
  }
  public GetProductByQuery(search:string,sortColumne:string,sortOrder:string):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT+search+'/'+sortColumne+'/'+sortOrder)
  }

  public AddProduct(product:Product):Observable<any>{
    return this.httpClient.post(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT,product)
  }

  public UpdateProduct(product:Product):Observable<any>{
    return this.httpClient.put(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT+product.productId,product)
  }

  public DeleteProduct(Id:number):Observable<any>{
    return this.httpClient.delete(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT+Id)
  }

  public GetProductByCountry(Id:number):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT_COUNTRY+Id)
  }

  public GetProductByCategory(Id:number):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT_CATEGORY+Id)
  }

  public GetProductByAdmin(Id:number):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.PRODUCT_ADMIN+Id)
  }




}
