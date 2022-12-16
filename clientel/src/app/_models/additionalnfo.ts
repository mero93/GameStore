export class AdditionalInfo {
    public constructor(init?: Partial<AdditionalInfo>) {
        Object.assign(this, init)
    }

    firstName: string;
    lastName: string;
    phone: string;
    paymentType: string;
    comment: string;
}