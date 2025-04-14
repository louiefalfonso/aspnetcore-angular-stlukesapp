import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Appointment } from '../models/appointment.models';
import { Observable, Subscription } from 'rxjs';
import { Doctor } from '../../doctor/models/doctor.models';
import { Patient } from '../../patient/models/patient.models';
import { AppointmentService } from '../services/appointment.service';
import { DoctorService } from '../../doctor/services/doctor.service';
import { PatientService } from '../../patient/services/patient.service';
import { UpdateAppointmentRequest } from '../models/update-appointment-request.models';

@Component({
  selector: 'app-edit-appointment',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './edit-appointment.component.html',
  styleUrl: './edit-appointment.component.css'
})
export class EditAppointmentComponent implements OnInit, OnDestroy {

  id: string | null = null;
  model?: Appointment;

  patients$? : Observable<Patient[]>;
  selectedPatients?: string[];
  doctors$? : Observable<Doctor[]>;
  selectedDoctors?: string[];

  // add Subscription
  routeSubscription?: Subscription;
  editAppointmentSubscription?: Subscription;
  getAppointmentSubscription?: Subscription;
  deleteAppointmentSubscription?: Subscription;

   // add constrcutor
   constructor(
    private patientService: PatientService,
    private doctorService: DoctorService,
    private appointmentService: AppointmentService,
    private route: ActivatedRoute, 
    private router: Router
  ) {}

  // add admission object
  appointment?: Appointment;

   // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    // get all patients
    this.patients$ = this.patientService.getAllPatients();

    // get all doctors
    this.doctors$ = this.doctorService.getAllDoctors();

    // get the id of the appointment to edit
   this.routeSubscription = this.route.paramMap.subscribe({
    next:(params) =>{
      this.id = params.get('id');

      if(this.id){
      this.getAppointmentSubscription =  this.appointmentService.getAppointmentById(this.id)
      .subscribe({
          next:(response)=>{
            this.model = response;
            this.selectedPatients = response.patients.map(x => x.id);
            this.selectedDoctors = response.doctors.map(x => x.id);
          }
        })
      }
    }
  })


  }

   // implement onFormSubmit
   onFormSubmit(): void {
    // convert this model to Request Object
    if(this.model && this.id){
      var updateAppointmentRequest : UpdateAppointmentRequest ={
        status: this.model?.status?? '',
        comments: this.model?.comments?? '',
        remarks: this.model?.remarks?? '',
        diagnosis: this.model?.diagnosis?? '',
        date: typeof this.model?.date === 'string' ? new Date(this.model.date) : this.model?.date ?? new Date(),
        patients: this.selectedPatients ?? [],
        doctors: this.selectedDoctors ?? [],
      }

      // pass this object to the service
      if(this.id){
        this.editAppointmentSubscription = this.appointmentService.updateAppointment(this.id, updateAppointmentRequest)
        .subscribe({
          next: (response) => {
            this.router.navigate(['/admin/appointments']);  
          },
          error: (error) => {
            console.error(error);
          }
        })
      }
    }
   }
   
  // implement onDelete
  onDelete():void{
    if(this.id){
     this.deleteAppointmentSubscription = this.appointmentService.deleteAppointment(this.id)
     .subscribe({
          next: (response) => {
            this.router.navigate(['/admin/appointments']);  
          },
          error: (error) => {
            console.error(error);
          }
      });
    }
  }

   // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
   this.routeSubscription?.unsubscribe();
   this.getAppointmentSubscription?.unsubscribe();
   this.editAppointmentSubscription?.unsubscribe();
   this.deleteAppointmentSubscription?.unsubscribe();
  }

}
