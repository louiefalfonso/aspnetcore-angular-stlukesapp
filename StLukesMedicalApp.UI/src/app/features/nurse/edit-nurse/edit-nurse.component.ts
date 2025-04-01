import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { Nurse } from '../models/nurse.models';
import { NurseService } from '../services/nurse.service';
import { UpdateNurseRequest } from '../models/update-nurse-request.models';

@Component({
  selector: 'app-edit-nurse',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './edit-nurse.component.html',
  styleUrl: './edit-nurse.component.css'
})
export class EditNurseComponent  implements OnInit, OnDestroy {

  // add id property
  id : string | null = null;
  
  // add Subscription
  routeSubscription?: Subscription;
  editNurseSubscription?: Subscription;
  deleteNurseSubscription? : Subscription;

  // add patient object
  model?: Nurse;

  // add constructor and inject the necessary services
  constructor(
    private nurseService: NurseService,
    private router: Router,
    private route : ActivatedRoute,) { } 


  // implement onFormSubmit 
  onFormSubmit():void {
    // convert this model to Request Object
    if (this.model && this.id) {
      var updateNurseRequest : UpdateNurseRequest ={
        firstName:this.model?.firstName?? '',
        lastName:this.model?.lastName?? '',
        emailAddress: this.model?.emailAddress?? '',
        phoneNumber: this.model?.phoneNumber?? '',
        badgeNumber: this.model?.badgeNumber?? '',
        schedule: this.model?.schedule?? '',
        department: this.model?.department?? '',
        qualifications: this.model?.qualifications?? ''
      }

      //pass this object to the service
      if(this.id){
        this.editNurseSubscription =  this.nurseService.updateNurse(this.id,updateNurseRequest)
        .subscribe({
            next: (response) => {
              this.router.navigate(['/admin/nurses']);  
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
       this.deleteNurseSubscription = this.nurseService.deleteNurse(this.id).subscribe({
          next: (response) => {
            this.router.navigate(['/admin/nurses']);  
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
          this.nurseService.getNurseById(this.id).subscribe({
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
    this.editNurseSubscription?.unsubscribe();
    this.deleteNurseSubscription?.unsubscribe();
  }

}
