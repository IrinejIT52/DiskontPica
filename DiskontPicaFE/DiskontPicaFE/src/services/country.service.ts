import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constant } from './constants';
import { Country } from '../models/county';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  constructor(private httpClient:HttpClient) { }

  public GetAllCountries():Observable<any>{
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.COUNTRY)
  }

  public GetCountryById(Id:number): Observable<any> {
    return this.httpClient.get(Constant.API_ENDPOINT+Constant.METHODS.COUNTRY+Id)
  }

  public AddCountry(country:Country): Observable<any>{
    return this.httpClient.post(Constant.API_ENDPOINT+Constant.METHODS.COUNTRY,country)
  }

  public UpdateCountry(country:Country): Observable<any>{
    return this.httpClient.put(Constant.API_ENDPOINT+Constant.METHODS.COUNTRY+country.countryId,country)
  }

  public DeleteCountry(Id:number): Observable<any>{
    return this.httpClient.delete(Constant.API_ENDPOINT+Constant.METHODS.COUNTRY+Id)
  }
}
