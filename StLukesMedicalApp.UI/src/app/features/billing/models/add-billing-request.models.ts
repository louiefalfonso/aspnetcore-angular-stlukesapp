export interface AddBillingRequest {
    totalAmount : string;
    paymentMethod: string;
    paymentStatus: string;
    dateOfBilling: Date;
    remarks: string;
    patients : string[];
}