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
import { PatientDetailComponent } from './features/patient/patient-detail/patient-detail.component';
import { AdmissionListComponent } from './features/admission/admission-list/admission-list.component';
import { AddAdmissionComponent } from './features/admission/add-admission/add-admission.component';
import { EditAdmissionComponent } from './features/admission/edit-admission/edit-admission.component';
import { DoctorDetailComponent } from './features/doctor/doctor-detail/doctor-detail.component';
import { NurseDetailComponent } from './features/nurse/nurse-detail/nurse-detail.component';
import { AdmissionDetailComponent } from './features/admission/admission-detail/admission-detail.component';
import { PrescriptionDetailComponent } from './features/prescription/prescription-detail/prescription-detail.component';
import { AppointmentListComponent } from './features/appointment/appointment-list/appointment-list.component';
import { AddAppointmentComponent } from './features/appointment/add-appointment/add-appointment.component';
import { EditAppointmentComponent } from './features/appointment/edit-appointment/edit-appointment.component';
import { AppointmentDetailComponent } from './features/appointment/appointment-detail/appointment-detail.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/auth/guards/auth.guard';

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
        canActivate: [authGuard] 
    },
    {
        path:"admin/patients/:id",
        component: EditPatientComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/patients/details/:id",
        component: PatientDetailComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/billings",
        component: BillingListComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/billings/add",
        component: AddBillingComponent,
        canActivate: [authGuard]  
    },
    {
        path:"admin/billings/:id",
        component: EditBillingComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/doctors",
        component: DoctorListComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/doctors/add",
        component: AddDoctorComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/doctors/:id",
        component: EditDoctorComponent,
        canActivate: [authGuard]  
    },
    {
        path:"admin/doctors/details/:id",
        component: DoctorDetailComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/nurses",
        component: NurseListComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/nurses/add",
        component: AddNurseComponent,
        canActivate: [authGuard]  
    },
    {
        path:"admin/nurses/:id",
        component: EditNurseComponent,
        canActivate: [authGuard]  
    },
    {
        path:"admin/nurses/details/:id",
        component: NurseDetailComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/prescriptions",
        component: PrescriptionListComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/prescriptions/add",
        component: AddPrescriptionComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/prescriptions/:id",
        component: EditPrescriptionComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/prescriptions/details/:id",
        component: PrescriptionDetailComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/admissions",
        component: AdmissionListComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/admissions/add",
        component: AddAdmissionComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/admissions/:id",
        component: EditAdmissionComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/admissions/details/:id",
        component: AdmissionDetailComponent,
        canActivate: [authGuard]  
    },
    {
        path:"admin/appointments",
        component: AppointmentListComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/appointments/add",
        component: AddAppointmentComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/appointments/:id",
        component: EditAppointmentComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"admin/appointments/details/:id",
        component: AppointmentDetailComponent, 
        canActivate: [authGuard] 
    },
    {
        path:"login",
        component: LoginComponent, 
    },

];
