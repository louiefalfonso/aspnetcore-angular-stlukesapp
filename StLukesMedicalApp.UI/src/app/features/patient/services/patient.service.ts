import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private http: HttpClient) { }
}