export interface UpdateBillingRequest {
    totalAmount : string;
    paymentMethod: string;
    paymentStatus: string;
    dateOfBilling: Date;
    patients : string[];
}