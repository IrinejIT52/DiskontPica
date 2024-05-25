import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constant } from './constants';
import { Order } from '../models/order';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private httpClinet:HttpClient) { }

  public CreateCheckOutSession(order:Order):Observable<any>{
    return this.httpClinet.post(Constant.API_STRIPE,order);
  }
}
