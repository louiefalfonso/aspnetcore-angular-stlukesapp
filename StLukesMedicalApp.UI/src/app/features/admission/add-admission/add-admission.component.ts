import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AddAdmissionRequest } from '../models/add-admission-request.models';
import { Observable, Subscription } from 'rxjs';
import { Doctor } from '../../doctor/models/doctor.models';
import { Patient } from '../../patient/models/patient.models';
import { AdmissionService } from '../services/admission.service';
import { HttpClient } from '@angular/common/http';
import { DoctorService } from '../../doctor/services/doctor.service';
import { PatientService } from '../../patient/services/patient.service';
import { Nurse } from '../../nurse/models/nurse.models';
import { NurseService } from '../../nurse/services/nurse.service';

@Component({
  selector: 'app-add-admission',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './add-admission.component.html',
  styleUrl: './add-admission.component.css'
})
export class AddAdmissionComponent implements OnInit, OnDestroy  {

  // add model
  model: AddAdmissionRequest;
  patients$? : Observable<Patient[]>;
  doctors$? : Observable<Doctor[]>;
  nurses$? : Observable<Nurse[]>;

  //add unsubcribe from observables
  private addAdmissionsSubscription ?: Subscription;

  // add constructor
  constructor(
    private admissionService: AdmissionService,
    private patientService: PatientService,
    private doctorService: DoctorService,
    private nurseService: NurseService,
    private router: Router,
    private http: HttpClient
  ){
    this.model = {
      roomNumber:'',
      roomType:'',
      availabilityStatus:'',
      remarks:'',
      date: new Date(),
      doctors:[],
      nurses:[],
      patients: []
    }
  }


  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    // display all patients
   this.patients$ = this.patientService.getAllPatients();

   // display all doctors
   this.doctors$ = this.doctorService.getAllDoctors();

   // display all nurses
   this.nurses$ = this.nurseService.getAllNurses();
  }

  // add onFormSubmit
  onFormSubmit() {
   this.addAdmissionsSubscription = this.admissionService.addNewAdmission(this.model)
    .subscribe({
      next: (response) => {
        this.router.navigate(['/admin/admissions']);  
      },
      error: (error) => {
        console.error(error);
      }
    })    
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
   this.addAdmissionsSubscription?.unsubscribe();
  }

}
