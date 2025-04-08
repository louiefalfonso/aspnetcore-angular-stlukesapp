export interface AddAdmissionRequest {
    roomNumber: string;
    roomType: string;
    remarks: string;
    availabilityStatus: string;
    date: Date;
    doctors: string[];
    nurses: string[];
    patients : string[];
}