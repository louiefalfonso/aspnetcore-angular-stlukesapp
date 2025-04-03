import { Doctor } from "../../doctor/models/doctor.models";
import { Patient } from "../../patient/models/patient.models"

export interface Prescription {
    id: string;
    medicationList: string;
    dosage: string;
    dateIssued: Date;
    remarks: string;
    patients: { id: string; firstName: string; lastName:string }[]; 
    doctors : Doctor[];
}