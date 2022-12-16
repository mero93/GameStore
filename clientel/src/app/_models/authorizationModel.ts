export interface AuthorizationModel {
    token: string;
    expiresAt: Date;
    refreshtoken: string;
}