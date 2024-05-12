import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constant } from './constants';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private httpClient:HttpClient) { }

  public GetAllOrders():Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ORDER)
  }

  public GetOrderById(Id:number):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ORDER+Id)
  }

  public AddOrder(order:Order):Observable<any>{
    return this.httpClient.post(Constant.API_ENDPOINT+Constant.METHODS.ORDER,order)
  }

  public UpdateOrder(order:Order): Observable<any>{
    return this.httpClient.put(Constant.API_ENDPOINT+Constant.METHODS.ORDER+order.orderId,order)
  }

  public DeleteOrder(Id:number):Observable<any>{
    return this.httpClient.delete(Constant.API_ENDPOINT+Constant.METHODS.ORDER+Id)
  }

  public GetOrderByCustomer(Id:number):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ORDER_CUSTOMER+Id)
  }

  
}
