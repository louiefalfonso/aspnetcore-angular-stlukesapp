import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Billing } from '../models/billing.models';
import { Observable, Subscription } from 'rxjs';
import { Patient } from '../../patient/models/patient.models';
import { PatientService } from '../../patient/services/patient.service';
import { BillingService } from '../services/billing.service';
import { UpdateBillingRequest } from '../models/update-biling-request.models';

@Component({
  selector: 'app-edit-billing',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './edit-billing.component.html',
  styleUrl: './edit-billing.component.css'
})
export class EditBillingComponent implements OnInit, OnDestroy {

  id: string | null = null;
  model?: Billing;
  patients$? : Observable<Patient[]>;
  selectedPatients?: string[];

  // add Subscription
  routeSubscription?: Subscription;
  editBillingSubscription?: Subscription;
  getBillingSubscription?: Subscription;
  deleteBillingSubscription?: Subscription;

  // add constrcutor
  constructor(
    private route: ActivatedRoute, 
    private billingService: BillingService,
    private patientService: PatientService,
    private router: Router
  ) {}

  // add billing object
  billing?: Billing;

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {

    this.patients$ = this.patientService.getAllPatients();

    // get the id of the billing to edit
    this.routeSubscription =  this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if(this.id){
          this.getBillingSubscription = this.billingService.getBillingById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedPatients = response.patients.map(x => x.id);
            }
          })
        }
      }
    });
  }

  // implement onFormSubmit
  onFormSubmit():void{
    
    // convert this model to Request Object
    if (this.model && this.id) {}
    var updateBillingRequest : UpdateBillingRequest = {
      totalAmount: this.model?.totalAmount?? '',
      paymentMethod: this.model?.paymentMethod?? '',
      paymentStatus: this.model?.paymentStatus?? '',
      dateOfBilling: typeof this.model?.dateOfBilling === 'string' ? new Date(this.model.dateOfBilling) : this.model?.dateOfBilling ?? new Date(),
      patients: this.selectedPatients ?? [],
      remarks:this.model?.remarks?? ''
    }

    //pass this object to the service
    if(this.id){
      this.editBillingSubscription =  this.billingService.updateBilling(this.id,updateBillingRequest)
      .subscribe({
          next: (response) => {
            this.router.navigate(['/admin/billings']);  
          },
          error: (error) => {
            console.error(error);
          }
        });
       }
  }

  // implement onDelete
  onDelete():void{
    if(this.id){
     this.deleteBillingSubscription = this.billingService.deleteBilling(this.id).subscribe({
          next: (response) => {
            this.router.navigate(['/admin/billings']);  
          },
          error: (error) => {
            console.error(error);
          }
      });
    }
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
   this.getBillingSubscription?.unsubscribe();
   this.editBillingSubscription?.unsubscribe();
   this.deleteBillingSubscription?.unsubscribe();
  }

}
