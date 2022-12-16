export interface Discussion {
    id: number;
    title: string;
    body: string;
    dateCreated: Date;
    dateUpdated: Date | null;
    lastActivity: Date;
    appUserId: number;
    username: string;
    photoUrl: string;
    gameId: number;
}