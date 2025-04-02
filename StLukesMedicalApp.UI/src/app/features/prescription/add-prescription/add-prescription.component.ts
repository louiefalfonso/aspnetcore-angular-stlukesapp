import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AddPrescriptionRequest } from '../models/add-prescription-request.models';
import { Prescription } from '../models/prescription.models';
import { Observable, Subscription } from 'rxjs';
import { PrescriptionService } from '../services/prescription.service';
import { PatientService } from '../../patient/services/patient.service';
import { HttpClient } from '@angular/common/http';
import { DoctorService } from '../../doctor/services/doctor.service';
import { Patient } from '../../patient/models/patient.models';
import { Doctor } from '../../doctor/models/doctor.models';

@Component({
  selector: 'app-add-prescription',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './add-prescription.component.html',
  styleUrl: './add-prescription.component.css'
})
export class AddPrescriptionComponent implements OnInit, OnDestroy {

  // add model
  model: AddPrescriptionRequest;
  patients$? : Observable<Patient[]>;
  doctors$? : Observable<Doctor[]>;
 
  //add unsubcribe from observables
   private addPrescriptionSubscription ?: Subscription;
   
// add constructor
constructor(
  private prescriptionService: PrescriptionService,
  private patientService: PatientService,
  private doctorService: DoctorService,
  private router: Router,
  private http: HttpClient
) {
  this.model = {
    medicationList:'',
    dosage:'',
    remarks:'',
    dateIssued: new Date(),
    doctors:[],
    patients: []
  }
}
   
  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
   // display all patients
   this.patients$ = this.patientService.getAllPatients();

   // display all doctors
   this.doctors$ = this.doctorService.getAllDoctors();
  }

  // add onFormSubmit
  onFormSubmit() {
    this.prescriptionService.addNewPresctiption(this.model)
    .subscribe({
      next: (response) => {
        this.router.navigate(['/admin/prescriptions']);  
      },
      error: (error) => {
        console.error(error);
      }
    })    
  }


  // implement ngOnDestroy lifecycle hook
   ngOnDestroy(): void {
   this.addPrescriptionSubscription?.unsubscribe();
  }

}
