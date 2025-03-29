import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { AddBillingRequest } from '../models/add-billing-request.models';
import { Observable, Subscription } from 'rxjs';
import { BillingService } from '../services/billing.service';
import { HttpClient } from '@angular/common/http';
import { Patient } from '../../patient/models/patient.models';
import { PatientService } from '../../patient/services/patient.service';


@Component({
  selector: 'app-add-billing',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './add-billing.component.html',
  styleUrl: './add-billing.component.css'
})
export class AddBillingComponent implements OnDestroy, OnInit {
  
  // add model
  model: AddBillingRequest;
  patients$? : Observable<Patient[]>;

  //add unsubcribe from observables
  private addBillingSubscription ?: Subscription;
  
  // add constructor
  constructor(
    private billingService: BillingService,
    private patientService: PatientService,
    private router: Router,
    private http: HttpClient
  ) {
    this.model = {
      totalAmount: '',
      paymentMethod: '',
      paymentStatus: '',
      dateOfBilling: new Date(),
      patients: []
    }
  }

  // implement ngOnInit lifecycle hook
    ngOnInit(): void {
      // display all patients
      this.patients$ = this.patientService.getAllPatients();
  }

  // add onFormSubmit
  onFormSubmit() {
    this.billingService.addBilling(this.model)
    .subscribe({
      next: (response) => {
        this.router.navigate(['/admin/billings']);  
      },
      error: (error) => {
        console.error(error);
      }
    })    
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
   this.addBillingSubscription?.unsubscribe();
  }

}
