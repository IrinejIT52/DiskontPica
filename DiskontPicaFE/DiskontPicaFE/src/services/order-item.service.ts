import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constant } from './constants';
import { OrderItem } from '../models/orderItem';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class OrderItemService {

  constructor(private httpClient:HttpClient) {  }

  public GetAllOrderItems(): Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ORDER_ITEM)
  }

  public GetOrderItemById(Id:number):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ORDER_ITEM+Id)
  }

  public AddOrderItem(orderItem:OrderItem):Observable<any>{
    return this.httpClient.post(Constant.API_ENDPOINT+Constant.METHODS.ORDER_ITEM,orderItem)
  }

  public UpdateOrderItem(orderItem:OrderItem):Observable<any>{
    return this.httpClient.put(Constant.API_ENDPOINT+Constant.METHODS.ORDER_ITEM+orderItem.orderItemId,orderItem)
  }

  public DeleteOrderItem(Id:number):Observable<any>{
    return this.httpClient.delete(Constant.API_ENDPOINT+Constant.METHODS.ORDER_ITEM+Id)
  }

  public GetOrderItemByOrder(Id:number):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ORDER_ITEM_ORDER+Id)
  }
  
}
