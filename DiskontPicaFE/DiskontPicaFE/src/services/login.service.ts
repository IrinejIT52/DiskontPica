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

  constructor() { }

  private http = inject(HttpClient);

  login(principal:Principal):Observable<any>{
    return this.http.post(Constant.API_LOGIN+Constant.METHODS.LOGIN,principal).pipe(
      tap(tokens=>this.doLoginUser(principal.name,tokens))
    )
  }

  private doLoginUser(name:string,tokens:any){
    this.loggedUser=name;
    this.storeJwtToken(tokens.jwt);
    this.isAuthSubject.next(true);

  }

  private storeJwtToken(jwt:string){
    localStorage.setItem(this.JWT_TOKEN,jwt);
  }

  logout(){
    localStorage.removeItem(this.JWT_TOKEN);
    this.isAuthSubject.next(false);
  }
}
