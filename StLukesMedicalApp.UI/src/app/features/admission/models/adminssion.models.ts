import { Doctor } from "../../doctor/models/doctor.models";
import { Nurse } from "../../nurse/models/nurse.models";
import { Patient } from "../../patient/models/patient.models";

export interface Admission {
    id: string;
    roomNumber: string;
    roomType: string;
    remarks: string;
    date: Date;
    doctors: Doctor;
    nurses: Nurse;
    patients :Patient;
}