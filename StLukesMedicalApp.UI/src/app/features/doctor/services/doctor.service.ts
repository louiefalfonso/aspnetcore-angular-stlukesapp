import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient,  HttpParams } from '@angular/common/http';
import { AddDoctorRequest } from '../models/add-doctor-request.models';
import { Observable } from 'rxjs';
import { Doctor } from '../models/doctor.models';
import { UpdateDoctorRequest } from '../models/update-doctor-request.models';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  // add constructor
  constructor(private http: HttpClient) { }

  // add new doctor
  addNewDoctor(model: AddDoctorRequest) : Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/doctors`, model);
  }

  // get all doctors
  getAllDoctors(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,): Observable<Doctor[]>{
    
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
  
      return this.http.get<Doctor[]>(`${environment.apiBaseUrl}/doctors`,{
        params: params
      });
      
  }

  // get doctor by ID
  getDoctorById(id: string) : Observable<Doctor>{
      return this.http.get<Doctor>(`${environment.apiBaseUrl}/doctors/${id}`);
  }

  // update doctor
  updateDoctor(id: string, updateDoctorRequest : UpdateDoctorRequest): Observable<Doctor>{
      return this.http.put<Doctor>(`${environment.apiBaseUrl}/doctors/${id}`, updateDoctorRequest);
  }

  // delete doctor
  deleteDoctor(id: string) : Observable<Doctor>{
      return this.http.delete<Doctor>(`${environment.apiBaseUrl}/doctors/${id}`);
  }

   // get doctor count
   getDoctorCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/doctors/count`);
  }
}
