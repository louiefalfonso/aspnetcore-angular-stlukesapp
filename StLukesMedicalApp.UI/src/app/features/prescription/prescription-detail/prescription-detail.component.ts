import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Prescription } from '../models/prescription.models';
import { Patient } from '../../patient/models/patient.models';
import { Doctor } from '../../doctor/models/doctor.models';
import { PrescriptionService } from '../services/prescription.service';

@Component({
  selector: 'app-prescription-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './prescription-detail.component.html',
  styleUrl: './prescription-detail.component.css'
})
export class PrescriptionDetailComponent implements OnInit {

  // add properties for patient, doctor and prescription
  PrescriptionsId!: string;

  prescriptions$?: Observable<Prescription>;
  patients$?: Observable<Patient[]>;
  doctors$?: Observable<Doctor[]>;

   // Constructor
   constructor(
    private prescriptionService: PrescriptionService,
    private route: ActivatedRoute
  ) {}

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    
    // get prescription Id from the route
    this.PrescriptionsId = this.route.snapshot.paramMap.get('id') || '';

    // fetch prescription details & return single object
    this.prescriptions$ = this.prescriptionService.getPrescriptionById(this.PrescriptionsId);

    // Fetch doctor filtered by the prescription ID
    this.doctors$ = this.prescriptions$.pipe(
      map(prescription => prescription?.doctors || []),
      catchError(error => {
        console.error('Error fetching doctors:', error);
        return of([]);
      })
    );

    // Fetch patients filtered by the admission ID
    this.patients$ = this.prescriptions$.pipe(
      map(prescription => prescription?.patients || []),
      catchError(error => {
        console.error('Error fetching patients:', error);
        return of([]);
      })
    );

  }

}
