import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AddAdmissionRequest } from '../models/add-admission-request.models';
import { Observable } from 'rxjs';
import { Admission } from '../models/adminssion.models';
import { UpdateAdmissionRequest } from '../models/update-admission-request.models';

@Injectable({
  providedIn: 'root'
})
export class AdmissionService {

  // add constructor
  constructor(private http: HttpClient) { }


  // add new admission
  addNewAdmission(model:AddAdmissionRequest) : Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/admissions`, model);
  }

  // get all admissions
  getAllAdmissions(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,): Observable<Admission[]>{
    
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

  return this.http.get<Admission[]>(`${environment.apiBaseUrl}/admissions`,{
    params: params
  });
  
  }

 // get admission by ID
 getAdmissionById(id: string) : Observable<Admission>{
  return this.http.get<Admission>(`${environment.apiBaseUrl}/admissions/${id}`);
 }

 // update admission
  updateAdmission(id: string, updateAdmissionRequest : UpdateAdmissionRequest): Observable<Admission>{
    return this.http.put<Admission>(`${environment.apiBaseUrl}/admissions/${id}`, updateAdmissionRequest);
  }

 // delete admission
 deleteAdmission(id: string) : Observable<Admission>{
  return this.http.delete<Admission>(`${environment.apiBaseUrl}/admissions/${id}`);
}

// get admission count
getAdmissionCount(): Observable<number> {
  return this.http.get<number>(`${environment.apiBaseUrl}/admissions/count`);
}


}
