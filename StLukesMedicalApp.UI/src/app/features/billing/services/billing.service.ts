import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { AddBillingRequest } from '../models/add-billing-request.models';
import { Observable } from 'rxjs';
import { Billing } from '../models/billing.models';
import { UpdateBillingRequest } from '../models/update-biling-request.models';

@Injectable({
  providedIn: 'root'
})
export class BillingService {

   // add constructor
   constructor(private http: HttpClient) { }

  // add new billing
  addBilling(model: AddBillingRequest) : Observable<Billing>{
    return this.http.post<Billing>(`${environment.apiBaseUrl}/billings`, model);
  }

  // get all bilings
  getAllBillings(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,): Observable<Billing[]>{
    
    let params = new HttpParams();
    
    if (query) {
      params = params.set('query', query)
    }

    if (sortBy) {
      params = params.set('sortBy', sortBy)
    }

    if (sortDirection) {
      params = params.set('sortDirection', sortDirection)
    }

    if (pageNumber) {
      params = params.set('pageNumber', pageNumber)
    }

    if (pageSize) {
      params = params.set('pageSize', pageSize)
    }

    return this.http.get<Billing[]>(`${environment.apiBaseUrl}/billings`,{
      params: params
    });
    
  }

  // get billing by ID
  getBillingById(id: string) : Observable<Billing>{
    return this.http.get<Billing>(`${environment.apiBaseUrl}/billings/${id}`);
  }

  // update billing
  updateBilling(id: string, updateBillingRequest : UpdateBillingRequest): Observable<Billing>{
    return this.http.put<Billing>(`${environment.apiBaseUrl}/billings/${id}`, updateBillingRequest);
  }

  // delete biiling
  deleteBilling(id: string) : Observable<Billing>{
    return this.http.delete<Billing>(`${environment.apiBaseUrl}/billings/${id}`);
  }

   // get billing count
   getBillingCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/billings/count`);
  }

}
