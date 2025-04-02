import { Routes } from '@angular/router';
import { PatientListComponent } from './features/patient/patient-list/patient-list.component';
import { HomeComponent } from './features/public/home/home.component';
import { AddPatientComponent } from './features/patient/add-patient/add-patient.component';
import { EditPatientComponent } from './features/patient/edit-patient/edit-patient.component';
import { BillingListComponent } from './features/billing/billing-list/billing-list.component';
import { AddBillingComponent } from './features/billing/add-billing/add-billing.component';
import { EditBillingComponent } from './features/billing/edit-billing/edit-billing.component';
import { DoctorListComponent } from './features/doctor/doctor-list/doctor-list.component';
import { AddDoctorComponent } from './features/doctor/add-doctor/add-doctor.component';
import { EditDoctorComponent } from './features/doctor/edit-doctor/edit-doctor.component';
import { NurseListComponent } from './features/nurse/nurse-list/nurse-list.component';
import { AddNurseComponent } from './features/nurse/add-nurse/add-nurse.component';
import { EditNurseComponent } from './features/nurse/edit-nurse/edit-nurse.component';
import { PrescriptionListComponent } from './features/prescription/prescription-list/prescription-list.component';
import { AddPrescriptionComponent } from './features/prescription/add-prescription/add-prescription.component';
import { EditPrescriptionComponent } from './features/prescription/edit-prescription/edit-prescription.component';

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
    {
        path:"admin/doctors",
        component: DoctorListComponent, 
    },
    {
        path:"admin/doctors/add",
        component: AddDoctorComponent, 
    },
    {
        path:"admin/doctors/:id",
        component: EditDoctorComponent, 
    },
    {
        path:"admin/nurses",
        component: NurseListComponent, 
    },
    {
        path:"admin/nurses/add",
        component: AddNurseComponent, 
    },
    {
        path:"admin/nurses/:id",
        component: EditNurseComponent, 
    },
    {
        path:"admin/prescriptions",
        component: PrescriptionListComponent, 
    },
    {
        path:"admin/prescriptions/add",
        component: AddPrescriptionComponent, 
    },
    {
        path:"admin/prescriptions/:id",
        component: EditPrescriptionComponent, 
    },
];
