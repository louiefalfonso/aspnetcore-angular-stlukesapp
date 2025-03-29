import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { Doctor } from '../models/doctor.models';
import { DoctorService } from '../services/doctor.service';
import { UpdateDoctorRequest } from '../models/update-doctor-request.models';

@Component({
  selector: 'app-edit-doctor',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './edit-doctor.component.html',
  styleUrl: './edit-doctor.component.css'
})
export class EditDoctorComponent implements OnInit, OnDestroy{

   // add id property
   id : string | null = null;

  // add Subscription
  routeSubscription?: Subscription;
  editDoctorSubscription?: Subscription;
  deleteDoctorSubscription? : Subscription;

  // add patient object
  model?: Doctor;

  // add constructor and inject the necessary services
  constructor(
    private doctorService: DoctorService,
    private router: Router,
    private route : ActivatedRoute,) { } 


  // implement onFormSubmit 
  onFormSubmit():void {

    // convert this model to Request Object
    if (this.model && this.id) {
      var updateDoctorRequest : UpdateDoctorRequest = {
        firstName:this.model?.firstName?? '',
        lastName:this.model?.lastName?? '',
        email: this.model?.email?? '',
        contactNumber: this.model?.contactNumber?? '',
        specialization: this.model?.specialization?? '',
        department: this.model?.department?? '',
        schedule: this.model?.schedule?? '',
      }
    
      //pass this object to the service
     if(this.id){
      this.editDoctorSubscription =  this.doctorService.updateDoctor(this.id,updateDoctorRequest)
      .subscribe({
          next: (response) => {
            this.router.navigate(['/admin/doctors']);  
            console.log("update doctor success")
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
       this.deleteDoctorSubscription = this.doctorService.deleteDoctor(this.id).subscribe({
          next: (response) => {
            this.router.navigate(['/admin/doctors']);  
          },
          error: (error) => {
            console.error(error);
          }
        });
      }
    }

  // implement ngOnInit lifecycle hook  
  ngOnInit(): void {
    
    // get the id of the doctor to edit
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
  
        if(this.id){
          this.doctorService.getDoctorById(this.id).subscribe({
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

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.editDoctorSubscription?.unsubscribe();
    this.deleteDoctorSubscription?.unsubscribe();
  }

   
}
