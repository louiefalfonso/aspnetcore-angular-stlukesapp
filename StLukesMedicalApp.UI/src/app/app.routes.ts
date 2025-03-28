import { Routes } from '@angular/router';
import { PatientListComponent } from './features/patient/patient-list/patient-list.component';
import { HomeComponent } from './features/public/home/home.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';
import { EditPatientComponent } from './features/patient/edit-patient/edit-patient.component';
import { BillingListComponent } from './features/billing/billing-list/billing-list.component';
import { AddBillingComponent } from './features/billing/add-billing/add-billing.component';
import { EditBillingComponent } from './features/billing/edit-billing/edit-billing.component';

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
    {
        path:"admin/patients/:id",
        component: EditPatientComponent, 
    },
    {
        path:"admin/billings",
        component: BillingListComponent, 
    },
    {
        path:"admin/billings/add",
        component: AddBillingComponent, 
    },
    {
        path:"admin/billings/:id",
        component: EditBillingComponent, 
    },
];
