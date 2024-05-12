import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constant } from './constants';
import { Administrator } from '../models/administrator';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private httpClient: HttpClient) { }

  public GetAllAdministrators(): Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ADMIN)
  }

  public GetAdminById(Id:number): Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ADMIN+Id)
  }

  public AddAdministrator(admin:Administrator): Observable<any>{
    return this.httpClient.post(Constant.API_ENDPOINT+Constant.METHODS.ADMIN,admin)
  }

  public UpdateAdministrator(admin:Administrator):Observable<any>{
    return this.httpClient.put(Constant.API_ENDPOINT+Constant.METHODS.ADMIN+admin.adminId,admin)
  }

  public DeleteAdministrator(Id:number):Observable<any>{
    return this.httpClient.delete(Constant.API_ENDPOINT+Constant.METHODS.ADMIN+Id)
  }

  public GetAdministratorByEmail(email:string):Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.ADMIN_EMAIL+email)
  }
}
