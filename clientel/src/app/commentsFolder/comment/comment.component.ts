import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActiveCommentInterface } from 'src/app/_models/activeCommentInterface';
import { ActiveCommentTypeEnum } from 'src/app/_models/activeCommentTypeEnum';
import { Comment } from 'src/app/_models/Comment';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})

export class CommentComponent implements OnInit {

  @Input() comment!: Comment;
  @Input() activeComment!: ActiveCommentInterface | null;
  @Input() currentUserId!: number;
  @Input() discussionId!: number;
  @Input() username: string;
  @Input() photoUrl: string

  @Output()
  setActiveComment = new EventEmitter<ActiveCommentInterface | null>();
  @Output()
  deleteComment = new EventEmitter<{ id: number; commentId: number | null}>();
  @Output()
  addComment = new EventEmitter<{ body: string; commentId: number | null }>();
  @Output()
  updateComment = new EventEmitter<{ body: string; id: number; commentId: number | null }>();

  canReply: boolean = false;
  canEdit: boolean = false;
  canDelete: boolean = false;
  activeCommentType = ActiveCommentTypeEnum;
  activeReply: ActiveCommentInterface | null = null;
  replyId: number | null = null;

  ngOnInit(): void {
    let roles: string[];
    let user = JSON.parse(localStorage.getItem('user'))
    if (user !== null) {
      roles = user['roles']
    }
    console.log(roles)
    this.canReply = Boolean(this.currentUserId);
    this.canEdit = this.currentUserId === this.comment.appUserId;
    this.canDelete =
      (this.currentUserId === this.comment.appUserId ||
        roles?.some(x => x === "ADMIN"))
    this.replyId = this.comment.commentId ? this.comment.commentId : this.comment.id;
    console.log('comment loaded', 'id', this.comment.id, 'reply id', this.replyId)
  }

  isReplying(): boolean {
    if (!this.activeComment) {
      return false;
    }
    return (
      this.activeComment.id === this.comment.id &&
      this.activeComment.type === this.activeCommentType.replying
    );
  }

  isEditing(): boolean {
    if (!this.activeComment) {
      return false;
    }
    return (
      this.activeComment.id === this.comment.id &&
      this.activeComment.type === 'editing'
    );
  }
}