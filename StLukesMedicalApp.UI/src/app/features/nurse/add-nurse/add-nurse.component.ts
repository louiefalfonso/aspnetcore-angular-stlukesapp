import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AddNurseRequest } from '../models/add-nurse-request.models';
import { Subscription } from 'rxjs';
import { NurseService } from '../services/nurse.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-nurse',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './add-nurse.component.html',
  styleUrl: './add-nurse.component.css'
})
export class AddNurseComponent implements OnDestroy{

  // add model
  model: AddNurseRequest;

 // add subcribe from observables
   private addNurseSubscription ?: Subscription;

  // add constructor
  constructor(
    private nurseService : NurseService,
    private http: HttpClient,
    private router: Router
  ) {
    this.model = {
      firstName: '',
      lastName:'',
      emailAddress: '',
      phoneNumber:'',
      badgeNumber:'',
      qualifications: '',
      schedule:'',
      department:''
    }
  }

  
  // add onFormSubmit
  onFormSubmit() {
    this.addNurseSubscription = this.nurseService.addNewNurse(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigate(['/admin/nurses']);  
        },
        error: (error) => {
          console.error(error);
        }
      });
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.addNurseSubscription?.unsubscribe();
  }

}
