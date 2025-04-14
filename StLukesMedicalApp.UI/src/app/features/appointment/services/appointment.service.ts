import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../../environments/environment';
import { AddAppointmentRequest } from '../models/add-appointment-request.models';
import { Observable } from 'rxjs';
import { Appointment } from '../models/appointment.models';
import { UpdateAppointmentRequest } from '../models/update-appointment-request.models';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  // add constructor
  constructor(private http: HttpClient) { }

  // add new appointment
  addNewAppointment(model: AddAppointmentRequest) : Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/appointments`, model);
  }

 // get all appointments
  getAllAppointments(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,): Observable<Appointment[]>{
    
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

  return this.http.get<Appointment[]>(`${environment.apiBaseUrl}/appointments`,{
    params: params
  });
  
  }

  // get appointment by ID
  getAppointmentById(id: string) : Observable<Appointment>{
  return this.http.get<Appointment>(`${environment.apiBaseUrl}/appointments/${id}`);
  }

  // update appointment
  updateAppointment(id: string, updateAppointmentRequest : UpdateAppointmentRequest): Observable<Appointment>{
    return this.http.put<Appointment>(`${environment.apiBaseUrl}/appointments/${id}`, updateAppointmentRequest);
  }

  // delete appointment
  deleteAppointment(id: string) : Observable<Appointment>{
    return this.http.delete<Appointment>(`${environment.apiBaseUrl}/appointments/${id}`);
  }

  // get appointment count
  getAppointmentCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/appointments/count`);
  }

}
