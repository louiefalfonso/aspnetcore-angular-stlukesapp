import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AddPrescriptionRequest } from '../models/add-prescription-request.models';
import { Observable } from 'rxjs';
import { Prescription } from '../models/prescription.models';
import { UpdatePrescriptionRequest } from '../models/update-prescription-request.models';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionService {

  // add constructor
  constructor(private http: HttpClient) { }

  // add new prescription
  addNewPresctiption(model: AddPrescriptionRequest) : Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/prescriptions`, model);
  }

  // get all prescriptions
  getAllPrescriptions(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,): Observable<Prescription[]>{
    
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

    return this.http.get<Prescription[]>(`${environment.apiBaseUrl}/prescriptions`,{
      params: params
    });
    
  }


  // get prescription by ID
  getPrescriptionById(id: string) : Observable<Prescription>{
    return this.http.get<Prescription>(`${environment.apiBaseUrl}/prescriptions/${id}`);
  }

  // update prescription
  updatePrescription(id: string, updatePrescriptionRequest : UpdatePrescriptionRequest): Observable<Prescription>{
    return this.http.put<Prescription>(`${environment.apiBaseUrl}/prescriptions/${id}`, updatePrescriptionRequest);
  }

  // delete prescription
  deletePrescription(id: string) : Observable<Prescription>{
    return this.http.delete<Prescription>(`${environment.apiBaseUrl}/prescriptions/${id}`);
  }

  // get billing count
  getPrescriptionCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/prescriptions/count`);
  }


}
