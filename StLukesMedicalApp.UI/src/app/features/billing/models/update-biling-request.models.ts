export interface UpdateBillingRequest {
    totalAmount : string;
    paymentMethod: string;
    paymentStatus: string;
    dateOfBilling: Date;
    remarks: string;
    patients : string[];
}