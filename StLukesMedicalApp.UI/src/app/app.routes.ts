import { Routes } from '@angular/router';
import { PatientListComponent } from './features/patient/patient-list/patient-list.component';
import { HomeComponent } from './features/public/home/home.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';

export const routes: Routes = [
    {
        path:"",
        component: HomeComponent,
        
    },
    {
        path:"admin/patients",
        component: PatientListComponent, 
    },
    {
        path:"admin/patients/add",
        component: AddPatientComponent, 
    },
];
