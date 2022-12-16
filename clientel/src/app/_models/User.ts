export class User {
    username: string;
    token: string;
    expiresAt: Date;
    refreshtoken: string;
    roles: string[];
    additionalInfo: boolean;
    photoUrl: string | undefined;
    publicId: string | undefined;
}