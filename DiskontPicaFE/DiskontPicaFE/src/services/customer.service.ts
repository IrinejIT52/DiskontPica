import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constant } from './constants';
import { Customer } from '../models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private httpClient:HttpClient) { }

  public GetAllCustomers(): Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.CUSTOMER)
  }

  public GetCustomerById(Id:number): Observable<any> {
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.CUSTOMER+Id)
  }

  public AddCustomer(customer:Customer): Observable<any>{
    return this.httpClient.post(Constant.API_ENDPOINT+Constant.METHODS.CUSTOMER,customer)
  }

  public UpdateCustomer(customer:Customer): Observable<any>{
    return this.httpClient.put(Constant.API_ENDPOINT+Constant.METHODS.CUSTOMER+customer.customerId,customer)
  }

  public DeleteCustomer(Id:number): Observable<any>{
    return this.httpClient.delete(Constant.API_ENDPOINT+Constant.METHODS.CUSTOMER+Id)
  }

  public GetCustomerByName(name:string):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.CUSTOMER_NAME+name)
  }
}
