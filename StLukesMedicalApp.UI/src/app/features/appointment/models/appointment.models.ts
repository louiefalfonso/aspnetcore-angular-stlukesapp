import { Doctor } from "../../doctor/models/doctor.models";
import { Patient } from "../../patient/models/patient.models"

export interface Appointment {
    id: string;
    status: string;
    comments: string;
    diagnosis: string;
    date: Date;
    remarks: string;
    patients: Patient []; 
    doctors : Doctor[];
}