import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constant } from './constants';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private httpClient: HttpClient) { }

  public GetAllCategories():Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.CATEGORY)
  }

  public GetCategoryById(Id:number): Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.CATEGORY+Id)
  }

  public AddCategory(category:Category): Observable<any>{
    return this.httpClient.post(Constant.API_ENDPOINT+Constant.METHODS.CATEGORY,category)
  }

  public UpdateCategory(category:Category): Observable<any>{
    return this.httpClient.put(Constant.API_ENDPOINT+Constant.METHODS.CATEGORY+category.categoryId,category)
  }

  public DeleteCategory(Id:number): Observable<any>{
    return this.httpClient.delete(Constant.API_ENDPOINT+Constant.METHODS.CATEGORY+Id)
  }
}
