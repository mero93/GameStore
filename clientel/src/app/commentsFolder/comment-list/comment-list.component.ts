import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ActiveCommentInterface } from 'src/app/_models/activeCommentInterface';
import { Comment } from 'src/app/_models/Comment';
import { CommentParams } from 'src/app/_models/CommentParams';
import { Pagination } from 'src/app/_models/pagination';
import { DiscussionsService } from 'src/app/_services/discussions.service';
import { decodeUserId } from 'src/app/_services/tokenHelpers';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.css']
})
export class CommentListComponent implements OnInit {
  @Input() discussionId: number

  private routeSub: Subscription;

  commentParams = new CommentParams();
  pagination: Pagination;
  currentUserId: number | null;
  comments: Comment[] = [];
  username: string;
  photoUrl: string;
  activeComment: ActiveCommentInterface | null = null;

  constructor(private discussionsService: DiscussionsService, private url: ActivatedRoute) {}

  ngOnInit(): void {
    let item = localStorage.getItem('user');
    if (item !== 'null')
    {
      let user = JSON.parse(item);
      this.currentUserId = Number(decodeUserId());
      this.username = user['username'];
      this.photoUrl = user['photoUrl'];
    }

    this.loadComments();
    console.log(this.currentUserId, this.username, this.photoUrl)
  }

  loadComments() {
    this.discussionsService.loadComments(this.commentParams, this.discussionId).subscribe(
      response => {
        this.comments = response.result;
        this.pagination = response.pagination;
      }
    );
  }

  updateComment({body, id, commentId,}: {body: string; id: number, commentId: number | null;}): void {

    let comment: Comment
    if (!commentId)
    {
      comment = this.comments.filter(x => x.id === id)[0];
      comment.body = body
      comment.dateUpdated = new Date();
    }

    else
    {
      comment = this.comments.filter(x => x.id === commentId)[0].replies.filter(x => x.id === id)[0]
      comment.body = body
      comment.dateUpdated = new Date();
    }
    

    this.discussionsService.updateComment(comment).subscribe();

    this.activeComment = null;
  }

  deleteComment({ id, commentId}: {id: number, commentId: number | null}) {
    this.discussionsService.deleteComment(id);

    if (!commentId)
    {
      this.comments = this.comments.filter(x => x.id !== id);
    }
    else
    {
      let comment = this.comments.filter(x => x.id === commentId)[0];
      comment.replies = comment.replies.filter(x => x.id !== id);
    }
  }

  setActiveComment(activeComment: ActiveCommentInterface | null):void {
    this.activeComment = activeComment;
  }

  
  addComment({body, commentId} : {body: string, commentId: number | null}): void {
    console.log('addComment', body, commentId)
    this.discussionsService.createComment(body, commentId, this.discussionId, this.username, this.photoUrl, this.currentUserId).subscribe(comment => 
    {
      if (!commentId)
      {
        this.comments.unshift(comment)
      }
      else
      {
        console.log('it goes to else statement')
        this.comments.filter(x => x.id === commentId)[0].replies.push(comment)
      }
    })
    this.activeComment = null;
  }

  pageChanged(event: any) {
    this.commentParams.pageNumber = event.page;
    this.loadComments();
  }
}
