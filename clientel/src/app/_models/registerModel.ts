export class RegisterModel {
    public constructor(init?: Partial<RegisterModel>) {
        Object.assign(this, init)
    }

    username: string;
    email: string;
    password: string;
    confirmpassword: string;
}