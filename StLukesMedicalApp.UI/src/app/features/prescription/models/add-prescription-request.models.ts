export interface AddPrescriptionRequest {
       medicationList: string;
       dosage: string;
       dateIssued: Date;
       remarks: string;
       patients : string[];
       doctors : string[];
}