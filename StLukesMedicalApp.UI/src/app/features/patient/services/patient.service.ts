import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';


import { environment } from '../../../../environments/environment';
import { AddPatientComponent } from '../add-patient/add-patient.component';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient.models';
import { UpdatePatientRequest } from '../models/update-patient-request.models';
import { AddPatientRequest } from '../models/add-patient-request.models';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  // add constructor
  constructor(private http: HttpClient) { }

  // add patient
  addPatient(model: AddPatientRequest) : Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/patients`, model);
  }

  // get all patients
  getAllPatients(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,):Observable<Patient[]>{

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

    return this.http.get<Patient[]>(`${environment.apiBaseUrl}/patients`,{
      params: params
      });

  }

   // get patient by id
   getPatientById(id: string): Observable<Patient>{
    return this.http.get<Patient>(`${environment.apiBaseUrl}/patients/${id}`);
  }

  // update patient
  updatePatient(id: string, updatePatientRequest: UpdatePatientRequest): Observable<Patient>{
    return this.http.put<Patient>(`${environment.apiBaseUrl}/patients/${id}`, updatePatientRequest);
  }

   // delete patient
   deletePatient(id: string): Observable<Patient>{
    return this.http.delete<Patient>(`${environment.apiBaseUrl}/patients/${id}`);
  }

  // get patient count
  getPatientCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/patients/count`);
  }
}



