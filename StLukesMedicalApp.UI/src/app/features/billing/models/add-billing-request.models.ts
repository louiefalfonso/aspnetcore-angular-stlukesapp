export interface AddBillingRequest{
    totalAmount : string;
    paymentMethod: string;
    paymentStatus: string;
    dateOfBilling: Date;
    patients : string[];
}