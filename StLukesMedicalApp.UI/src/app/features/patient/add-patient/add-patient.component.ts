import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AddPatientRequest } from '../models/add-patient-request.models';
import { PatientService } from '../services/patient.service';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-patient',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './add-patient.component.html',
  styleUrl: './add-patient.component.css'
})
export class AddPatientComponent implements OnDestroy{
 
  // add model
  model: AddPatientRequest;

  // add unsubcribe from observables
 private addPatientSubscription ?: Subscription;

  // add constructor
  constructor(
    private patientService : PatientService,
    private http: HttpClient,
    private router: Router){
    this.model = {
      firstName: '',
      lastName: '',
      age: '',
      email: '',
      contactNumber: '',
      sex: '',
      address: '',
      diagnosis: '',
      patientType: '',
    }
   }

  
  // add onFormSubmit
 onFormSubmit() {
  this.addPatientSubscription = this.patientService.addPatient(this.model)
    .subscribe({
      next: (response) => {
        this.router.navigate(['/admin/patients']);  
      },
      error: (error) => {
        console.error(error);
      }
    });
}


  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.addPatientSubscription?.unsubscribe();
  }
}
