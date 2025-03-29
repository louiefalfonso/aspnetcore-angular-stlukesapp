import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AddDoctorRequest } from '../models/add-doctor-request.models';
import { Subscription } from 'rxjs';
import { DoctorService } from '../services/doctor.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-doctor',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './add-doctor.component.html',
  styleUrl: './add-doctor.component.css'
})
export class AddDoctorComponent implements OnDestroy{

  // add model
  model: AddDoctorRequest;

  // add subcribe from observables
   private addDoctorSubscription ?: Subscription;
  

  // add constructor
  constructor(
    private doctorService : DoctorService,
    private http: HttpClient,
    private router: Router
  ){
    this.model ={
      firstName: '',
      lastName:'',
      email: '',
      contactNumber: '',
      specialization: '',
      department: '',
      schedule:''
    }
  }

  // add onFormSubmit
  onFormSubmit() {
    this.addDoctorSubscription = this.doctorService.addNewDoctor(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigate(['/admin/doctors']);  
        },
        error: (error) => {
          console.error(error);
        }
      });
  }

  // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.addDoctorSubscription?.unsubscribe();
  }

}
