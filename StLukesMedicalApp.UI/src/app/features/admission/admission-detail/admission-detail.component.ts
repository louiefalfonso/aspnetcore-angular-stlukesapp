import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { Observable, of, Subscription, switchMap } from 'rxjs';
import { AdmissionService } from '../services/admission.service';
import { Admission } from '../models/adminssion.models';
import { Patient } from '../../patient/models/patient.models';
import { PatientService } from '../../patient/services/patient.service';
import { Doctor } from '../../doctor/models/doctor.models';
import { DoctorService } from '../../doctor/services/doctor.service';
import { Nurse } from '../../nurse/models/nurse.models';
import { NurseService } from '../../nurse/services/nurse.service';

@Component({
  selector: 'app-admission-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './admission-detail.component.html',
  styleUrl: './admission-detail.component.css',
})
export class AdmissionDetailComponent implements OnInit{

  // add properties for patientId, doctorId, billingId and admissionId
  AdmissionsId!:string;
  PatientsId!: string; 
  DoctorsId!:string;
  NursesId!:string

  admissions$?: Observable<Admission>; 
  patients$?: Observable<Patient[]>;
  doctors$?: Observable<Doctor[]>;
  nurses$?: Observable<Nurse[]>;
 
  // Constructor
  constructor(
    private admissionService: AdmissionService,
    private route: ActivatedRoute
  ) {}

  // Lifecycle hook: OnInit
  ngOnInit(): void {
    
     // Get the admission ID from the route parameters
    this.AdmissionsId = this.route.snapshot.paramMap.get('id') || '';

    // Fetch admission details (return a single admission object)
    this.admissions$ = this.admissionService.getAdmissionById(this.AdmissionsId)

     // Fetch patients filtered by the admission ID
     this.patients$ = this.admissions$.pipe(
      map(admission => admission?.patients || []),
      catchError(error => {
        console.error('Error fetching patients:', error);
        return of([]);
      })
    );

    // Fetch doctors filtered by the admission ID
    this.doctors$ = this.admissions$.pipe(
      map(admission => admission?.doctors || []),
      catchError(error => {
        console.error('Error fetching doctors:', error);
        return of([]);
      })
    );

    // Fetch nurses filtered by the admission ID
    this.nurses$ = this.admissions$.pipe(
      map(admission => admission?.nurses || []),
      catchError(error => {
        console.error('Error fetching nurses:', error);
        return of([]);
      })
    );

  }

  

}