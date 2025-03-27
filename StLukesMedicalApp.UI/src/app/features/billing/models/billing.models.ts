import { Patient } from "../../patient/models/patient.models";

export interface Billing {
    id: string;
    totalAmount : string;
    paymentMethod: string;
    paymentStatus: string;
    dateOfBilling: Date;
    patients : Patient[];
}
