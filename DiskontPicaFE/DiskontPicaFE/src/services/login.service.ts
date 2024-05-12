import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Principal } from '../models/principal';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Constant } from './constants';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private readonly JWT_TOKEN='JWT_TOKEN'
  private loggedUser?:string
  private isAuthSubject = new BehaviorSubject<boolean>(false)

  constructor(private httpClient:HttpClient) { }

  

  login(principal:Principal):Observable<any>{
    return this.httpClient.post(Constant.API_LOGIN+Constant.METHODS.LOGIN,principal);
  } 
 


}
