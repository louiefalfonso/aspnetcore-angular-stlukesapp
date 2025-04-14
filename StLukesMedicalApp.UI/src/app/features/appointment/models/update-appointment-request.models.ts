export interface UpdateAppointmentRequest {
    status: string;
    comments: string;
    diagnosis: string;
    date: Date;
    remarks: string;
    patients: string []; 
    doctors : string[];
}