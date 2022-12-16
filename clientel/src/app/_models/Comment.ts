export interface Comment {
    id: number;
    body: string;
    dateCreated: Date;
    dateUpdated: Date | null
    appUserId: number;
    username: string;
    photoUrl: string;
    discussionId: number;
    commentId: number | null;
    replies: Comment[]
}