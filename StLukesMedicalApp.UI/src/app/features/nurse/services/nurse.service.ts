import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddNurseRequest } from '../models/add-nurse-request.models';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { Nurse } from '../models/nurse.models';
import { UpdateNurseRequest } from '../models/update-nurse-request.models';

@Injectable({
  providedIn: 'root'
})
export class NurseService {

  // add constructor
  constructor(private http: HttpClient) { }

  // add nurse
  addNewNurse(model: AddNurseRequest) : Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/nurses`, model);
  }

  // get all nurses
  getAllNurses(query?: string, sortBy?: string, sortDirection?: string, pageNumber?: number, pageSize?: number,):Observable<Nurse[]>{

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

    return this.http.get<Nurse[]>(`${environment.apiBaseUrl}/nurses`,{
      params: params
      });

  }

  // get nurse by id
   getNurseById(id: string): Observable<Nurse>{
    return this.http.get<Nurse>(`${environment.apiBaseUrl}/nurses/${id}`);
  }

  // update nurse
  updateNurse(id: string, updateNurseRequest: UpdateNurseRequest): Observable<Nurse>{
    return this.http.put<Nurse>(`${environment.apiBaseUrl}/nurses/${id}`, updateNurseRequest);
  }

   // delete nurse
   deleteNurse(id: string): Observable<Nurse>{
    return this.http.delete<Nurse>(`${environment.apiBaseUrl}/nurses/${id}`);
  }

  // get nurse count
  getNurseCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/nurses/count`);
  }

}
