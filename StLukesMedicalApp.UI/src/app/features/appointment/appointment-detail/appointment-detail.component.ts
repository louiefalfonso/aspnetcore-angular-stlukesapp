import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Appointment } from '../models/appointment.models';
import { Doctor } from '../../doctor/models/doctor.models';
import { Patient } from '../../patient/models/patient.models';
import { AppointmentService } from '../services/appointment.service';

@Component({
  selector: 'app-appointment-detail',
  imports: [RouterModule, CommonModule],
  templateUrl: './appointment-detail.component.html',
  styleUrl: './appointment-detail.component.css'
})
export class AppointmentDetailComponent implements OnInit{

  // add properties for patient, doctor and appointment
  PatientsId!: string; 
  DoctorsId!:string;
  AppointmentId!:string;

  patients$?: Observable<Patient[]>;
  doctors$?: Observable<Doctor[]>;
  appointments$?: Observable<Appointment>


  // Constructor
  constructor(
    private appointmentService: AppointmentService,
    private route: ActivatedRoute
  ) {}

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {
    
    // Get the appointment ID from the route parameters
    this.AppointmentId = this.route.snapshot.paramMap.get('id') || '';

    // Fetch appointment details (return a single appointment object)
    this.appointments$ = this.appointmentService.getAppointmentById(this.AppointmentId)

    // Fetch patients filtered by the appointment ID
    this.patients$ = this.appointments$.pipe(
      map(appointment => appointment?.patients || []),
        catchError(error => {
          console.error('Error fetching patients:', error);
          return of([]);
      })
    );

    // Fetch doctors filtered by the appointment ID
    this.doctors$ = this.appointments$.pipe(
      map(appointment => appointment?.doctors || []),
        catchError(error => {
          console.error('Error fetching doctors:', error);
          return of([]);
      })
    );

  }

}
