import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Prescription } from '../models/prescription.models';
import { Patient } from '../../patient/models/patient.models';
import { Observable, Subscription } from 'rxjs';
import { Doctor } from '../../doctor/models/doctor.models';
import { PrescriptionService } from '../services/prescription.service';
import { PatientService } from '../../patient/services/patient.service';
import { DoctorService } from '../../doctor/services/doctor.service';
import { UpdatePrescriptionRequest } from '../models/update-prescription-request.models';

@Component({
  selector: 'app-edit-prescription',
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './edit-prescription.component.html',
  styleUrl: './edit-prescription.component.css'
})
export class EditPrescriptionComponent implements OnInit, OnDestroy{

  id: string | null = null;
  model?: Prescription;
  patients$? : Observable<Patient[]>;
  selectedPatients?: string[];
  doctors$? : Observable<Doctor[]>;
  selectedDoctors?: string[];


  // add Subscription
  routeSubscription?: Subscription;
  editPrescriptionSubscription?: Subscription;
  getPrescriptionSubscription?: Subscription;
  deletePrescriptionSubscription?: Subscription;

  // add constrcutor
  constructor(
    private route: ActivatedRoute, 
    private prescriptionService: PrescriptionService,
    private patientService: PatientService,
    private doctorService: DoctorService,
    private router: Router
  ) {}

  // add prescription object
  prescription?: Prescription;

  // implement ngOnInit lifecycle hook
  ngOnInit(): void {

    // get all patients
    this.patients$ = this.patientService.getAllPatients();

    // get all doctors
    this.doctors$ = this.doctorService.getAllDoctors();

    // get the id of the billing to edit
    this.routeSubscription =  this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if(this.id){
          this.getPrescriptionSubscription = this.prescriptionService.getPrescriptionById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedPatients = response.patients.map(x => x.id);
              this.selectedDoctors = response.doctors.map(x => x.id);
            }
          })
        }
      }
    });
  }

  // implement onFormSubmit
  onFormSubmit():void{
    // convert this model to Request Object
    if (this.model && this.id) {
      var updatePrescriptionRequest : UpdatePrescriptionRequest = {
        medicationList: this.model?.medicationList?? '',
        dateIssued: typeof this.model?.dateIssued === 'string' ? new Date(this.model.dateIssued) : this.model?.dateIssued ?? new Date(),
        dosage: this.model?.dosage?? '',
        remarks: this.model?.remarks?? '',
        patients: this.selectedPatients ?? [],
        doctors: this.selectedDoctors ?? [],
      }

    // pass this object to the service
    if(this.id){
      this.editPrescriptionSubscription =  this.prescriptionService.updatePrescription(this.id,updatePrescriptionRequest)
      .subscribe({
          next: (response) => {
            this.router.navigate(['/admin/prescriptions']);  
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
     this.deletePrescriptionSubscription = this.prescriptionService.deletePrescription(this.id).subscribe({
          next: (response) => {
            this.router.navigate(['/admin/prescriptions']);  
          },
          error: (error) => {
            console.error(error);
          }
      });
    }
  }

   // implement ngOnDestroy lifecycle hook
  ngOnDestroy(): void {
    this.getPrescriptionSubscription?.unsubscribe();
    this.routeSubscription?.unsubscribe();
    this.editPrescriptionSubscription?.unsubscribe();
    this.deletePrescriptionSubscription?.unsubscribe();
  }

}
