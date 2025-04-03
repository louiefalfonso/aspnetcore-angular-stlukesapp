import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { PrescriptionService } from '../../prescription/services/prescription.service';
import { Prescription } from '../../prescription/models/prescription.models';
import { BillingService } from '../../billing/services/billing.service';
import { Billing } from '../../billing/models/billing.models';
import { Patient } from '../models/patient.models';
import { PatientService } from '../services/patient.service';

@Component({
  selector: 'app-patient-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './patient-detail.component.html',
  styleUrl: './patient-detail.component.css'
})
export class PatientDetailComponent implements OnInit{

  PatientsId!: string; 
  DoctorsId!:string;
  BillingsId!:string;

  prescriptions$?: Observable<Prescription[]>;
  billings$?: Observable<Billing[]>;
  patients$?: Observable<Patient>;

  // add Subscription
  routeSubscription?: Subscription;

  constructor(
    private prescriptionService: PrescriptionService,
    private billingService: BillingService,
    private patientService: PatientService,
    private route: ActivatedRoute
  ) { }

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {

     // Get the patient ID from the route parameters
    this.PatientsId = this.route.snapshot.paramMap.get('id') || '';
    this.DoctorsId = this.route.snapshot.paramMap.get('doctorId') || '';
    this.BillingsId = this.route.snapshot.paramMap.get('billingId') || '';

    // Fetch prescriptions filtered by the patient ID
    this.prescriptions$ = this.prescriptionService.getAllPrescriptions().pipe(
      map(prescriptions =>
        prescriptions.filter(prescription =>
          prescription.patients.some(patient => patient.id === this.PatientsId) 
        )
      )
    );

    // Fetch billing filtered by the patient ID
    this.billings$ = this.billingService.getAllBillings().pipe(
      map(billings =>
        billings.filter(billing =>
          billing.patients.some(patient => patient.id === this.PatientsId)
        )
      ) 
    );

   // Fetch patient details (return a single Patient object)
    this.patients$ = this.patientService.getPatientById(this.PatientsId);
  }
}