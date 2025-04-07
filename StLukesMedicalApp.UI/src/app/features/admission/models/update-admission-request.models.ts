export interface UpdateAdmissionRequest{
    roomNumber: string;
    roomType: string;
    remarks: string;
    date: Date;
    doctors: string;
    nurses: string;
    patients : string;
}