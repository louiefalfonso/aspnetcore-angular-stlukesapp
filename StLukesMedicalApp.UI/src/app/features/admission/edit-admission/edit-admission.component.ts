import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Admission } from '../models/adminssion.models';
import { Observable, Subscription } from 'rxjs';
import { Doctor } from '../../doctor/models/doctor.models';
import { Patient } from '../../patient/models/patient.models';
import { Nurse } from '../../nurse/models/nurse.models';
import { NurseService } from '../../nurse/services/nurse.service';
import { DoctorService } from '../../doctor/services/doctor.service';
import { PatientService } from '../../patient/services/patient.service';
import { AdmissionService } from '../services/admission.service';
import { UpdateAdmissionRequest } from '../models/update-admission-request.models';

@Component({
  selector: 'app-edit-admission',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './edit-admission.component.html',
  styleUrl: './edit-admission.component.css'
})
export class EditAdmissionComponent implements OnInit, OnDestroy{

  id: string | null = null;
  model?: Admission;
  
  patients$? : Observable<Patient[]>;
  selectedPatients?: string[];
  doctors$? : Observable<Doctor[]>;
  selectedDoctors?: string[];
  nurses$?: Observable<Nurse[]>
  selectedNurses?: string[];

  // add Subscription
  routeSubscription?: Subscription;
  editAdmissionSubscription?: Subscription;
  getAdmissionSubscription?: Subscription;
  deleteAdmissionSubscription?: Subscription;

   // add constrcutor
   constructor(
    private route: ActivatedRoute, 
    private nurseService: NurseService,
    private patientService: PatientService,
    private doctorService: DoctorService,
    private admissionService: AdmissionService,
    private router: Router
  ) {}

  // add admission object
  admission?: Admission;

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {

    // get all patients
    this.patients$ = this.patientService.getAllPatients();

    // get all doctors
    this.doctors$ = this.doctorService.getAllDoctors();

    // get all nurses
    this.nurses$ = this.nurseService.getAllNurses();

    // get the id of the admission to edit
   this.routeSubscription = this.route.paramMap.subscribe({
      next:(params) =>{
        this.id = params.get('id');

        if(this.id){
        this.getAdmissionSubscription =  this.admissionService.getAdmissionById(this.id).subscribe({
            next:(response)=>{
              this.model = response;
              this.selectedPatients = response.patients.map(x => x.id);
              this.selectedDoctors = response.doctors.map(x => x.id);
              this.selectedNurses = response.nurses.map(x => x.id);
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
      var updateAdmissionRequest : UpdateAdmissionRequest = {
        roomType: this.model?.roomType?? '',
        roomNumber:this.model?.roomNumber?? '',
        remarks: this.model?.remarks?? '',
        availabilityStatus: this.model?.availabilityStatus?? '',
        date: typeof this.model?.date === 'string' ? new Date(this.model.date) : this.model?.date ?? new Date(),
        patients: this.selectedPatients ?? [],
        doctors: this.selectedDoctors ?? [],
        nurses: this.selectedNurses ?? [],
      }

      // pass this object to the service
    if(this.id){
      this.editAdmissionSubscription = this.admissionService.updateAdmission(this.id,updateAdmissionRequest)
      .subscribe({
          next: (response) => {
            this.router.navigate(['/admin/admissions']);  
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
     this.deleteAdmissionSubscription = this.admissionService.deleteAdmission(this.id)
     .subscribe({
          next: (response) => {
            this.router.navigate(['/admin/admissions']);  
          },
          error: (error) => {
            console.error(error);
          }
      });
    }
  }

   // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.getAdmissionSubscription?.unsubscribe();
    this.routeSubscription?.unsubscribe();
    this.editAdmissionSubscription?.unsubscribe();
    this.deleteAdmissionSubscription?.unsubscribe();
  }

}
