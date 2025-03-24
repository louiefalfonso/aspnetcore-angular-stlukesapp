import { Routes } from '@angular/router';
import { HomeComponent } from './features/public/home/home.component';
import { AdmissionComponent } from './features/public/admission/admission.component';
import { PatientListComponent } from './features/patient/patient-list/patient-list.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';

export const routes: Routes = [
   {
        path: "",
        component: HomeComponent
   },
   {
        path: "admissions",
        component: AdmissionComponent
   },
   {
     path: "admin/patients",
     component: PatientListComponent
   },
   {
     path: "admin/patients/add",
     component: AddPatientComponent
   },
];
