import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AdmissionService } from '../services/admission.service';
import { Admission } from '../models/adminssion.models';
import { Patient } from '../../patient/models/patient.models';
import { Doctor } from '../../doctor/models/doctor.models';
import { Nurse } from '../../nurse/models/nurse.models';

@Component({
  selector: 'app-admission-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './admission-detail.component.html',
  styleUrl: './admission-detail.component.css',
})
export class AdmissionDetailComponent implements OnInit{

  // add properties for patient, doctor and admission
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

  // implement ngOnInit lifecycle hook
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