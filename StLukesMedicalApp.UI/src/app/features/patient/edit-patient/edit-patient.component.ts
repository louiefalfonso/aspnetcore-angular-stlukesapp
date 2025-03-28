import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, ActivatedRoute, Router} from '@angular/router';
import { Subscription } from 'rxjs';
import { Patient } from '../models/patient.models';
import { PatientService } from '../services/patient.service';
import { UpdatePatientRequest } from '../models/update-patient-request.models';

@Component({
  selector: 'app-edit-patient',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './edit-patient.component.html',
  styleUrl: './edit-patient.component.css'
})
export class EditPatientComponent implements OnInit, OnDestroy {

  // add id property
  id : string | null = null;
  
  // add Subscription
  paramsSubscription?: Subscription;
  editPatientSubscription?: Subscription;
  deletePatientSubscription? : Subscription;

  // add patient object
  model?: Patient;

  // add constructor and inject the necessary services
  constructor(
      private patientService: PatientService,
      private router: Router,
      private route : ActivatedRoute,) { } 

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
   // get the id of the patient to edit
  this.paramsSubscription = this.route.paramMap.subscribe({
    next: (params) => {
      this.id = params.get('id');

      if(this.id){
        this.patientService.getPatientById(this.id).subscribe({
          next: (response)=>{
            this.model= response
          },
          error: (error) => {
            console.error(error);
          }
        })
      }
    }
   })
  }

   // implement onFormSubmit
   onFormSubmit():void{

    if (this.model && this.id) {
      var updatePatientRequest : UpdatePatientRequest ={
        firstName: this.model?.firstName?? '',
        lastName:this.model?.lastName?? '',
        email: this.model?.email?? '',
        contactNumber: this.model?.contactNumber?? '',
        sex: this.model?.sex?? '',
        age: this.model?.age?? '',
        address: this.model?.address?? '',
        diagnosis: this.model?.diagnosis?? '',
        patientType: this.model?.patientType?? '',
      }

      //pass this object to the service
     if(this.id){
      this.editPatientSubscription =  this.patientService.updatePatient(this.id,updatePatientRequest)
      .subscribe({
          next: (response) => {
            this.router.navigate(['/admin/patients']);  
          },
          error: (error) => {
            console.error(error);
          }
        });
       }
    }
   }

    // implement onDelete
    onDelete():void{
      if(this.id){
       this.deletePatientSubscription = this.patientService.deletePatient(this.id).subscribe({
          next: (response) => {
            this.router.navigate(['/admin/patients']);  
          },
          error: (error) => {
            console.error(error);
          }
        });
      }
    }


 // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editPatientSubscription?.unsubscribe();
    this.deletePatientSubscription?.unsubscribe();
  }


}
