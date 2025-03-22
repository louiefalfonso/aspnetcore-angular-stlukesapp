import { Routes } from '@angular/router';
import { HomeComponent } from './features/public/home/home.component';
import { AdmissionComponent } from './features/public/admission/admission.component';

export const routes: Routes = [
   {
        path: "",
        component: HomeComponent
   },
   {
        path: "admissions",
        component: AdmissionComponent
   },
];
