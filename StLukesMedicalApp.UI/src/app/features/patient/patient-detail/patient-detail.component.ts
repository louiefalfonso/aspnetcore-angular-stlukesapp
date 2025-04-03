import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Patient } from '../models/patient.models';
import { PatientService } from '../services/patient.service';
import { PrescriptionService } from '../../prescription/services/prescription.service';
import { Prescription } from '../../prescription/models/prescription.models';

@Component({
  selector: 'app-patient-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './patient-detail.component.html',
  styleUrl: './patient-detail.component.css'
})
export class PatientDetailComponent implements OnInit {

  PatientsId!: string; // Store the current patient's ID
  DoctorsId!:string;
  prescriptions$?: Observable<Prescription[]>;

  constructor(
    private prescriptionService: PrescriptionService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
     // Get the patient ID and doctor ID from the route parameters
    this.PatientsId = this.route.snapshot.paramMap.get('id') || '';
    this.DoctorsId = this.route.snapshot.paramMap.get('doctorId') || '';
  
    // Fetch prescriptions filtered by the patient ID
    this.prescriptions$ = this.prescriptionService.getAllPrescriptions().pipe(
      map(prescriptions =>
        prescriptions.filter(prescription =>
          prescription.patients.some(patient => patient.id === this.PatientsId) 
        )
      )
    );
  }
}