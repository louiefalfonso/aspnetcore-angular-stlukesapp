import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AddAppointmentRequest } from '../models/add-appointment-request.models';
import { Observable, Subscription } from 'rxjs';
import { Doctor } from '../../doctor/models/doctor.models';
import { Patient } from '../../patient/models/patient.models';
import { AppointmentService } from '../services/appointment.service';
import { HttpClient } from '@angular/common/http';
import { PatientService } from '../../patient/services/patient.service';
import { DoctorService } from '../../doctor/services/doctor.service';

@Component({
  selector: 'app-add-appointment',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './add-appointment.component.html',
  styleUrl: './add-appointment.component.css'
})
export class AddAppointmentComponent implements OnInit, OnDestroy{


  // add model
  model: AddAppointmentRequest;
  patients$? : Observable<Patient[]>;
  doctors$? : Observable<Doctor[]>;

  //add unsubcribe from observables
  private addAppointmentSubscription ?: Subscription;

 // Add toast visibility property
 showToast: boolean = false;

  // add constructor
  constructor(
    private appointmentService: AppointmentService,
    private patientService: PatientService,
    private doctorService: DoctorService,
    private router: Router,
    private http: HttpClient
  ) { 
    this.model = {
      status:'',
      comments:'',
      diagnosis:'',
      remarks:'',
      date: new Date(),
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
    this.appointmentService.addNewAppointment(this.model)
    .subscribe({
      next: (response) => {
        this.showToast = true; 
        setTimeout(() => {
          this.showToast = false;
          this.router.navigate(['/admin/appointment']);
        }, 2000);
      },
      error: (error) => {
        console.error(error);
      }
    })    
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.addAppointmentSubscription?.unsubscribe();
  }

}
