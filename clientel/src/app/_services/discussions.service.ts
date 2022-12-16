import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, take } from 'rxjs';
import { Comment } from '../_models/Comment';
import { CommentParams } from '../_models/CommentParams';
import { Discussion } from '../_models/discussion';
import { DiscussionParams } from '../_models/DiscussionParams';
import { PaginatedResult } from '../_models/pagination';
import { getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class DiscussionsService {
  
  constructor(private http: HttpClient) { }
  paginatedComments: PaginatedResult<Comment[]> = new PaginatedResult<Comment[]>();
  paginatedDiscussions: PaginatedResult<Discussion[]> = new PaginatedResult<Discussion[]>();
  commentParams = new CommentParams();
  discussionParams = new DiscussionParams();

  loadDiscussions(discussionParams: DiscussionParams, gameId: number | null) {
    let params = getPaginationHeaders(discussionParams.pageNumber, discussionParams.pageSize);

    return this.http.get<Discussion[]>('https://localhost:5001/api/Discussions/' + (gameId? gameId : ""), {observe: 'response', params})
    .pipe(map(response => {
      this.paginatedDiscussions.result = response.body;
      if (response.headers.get('Pagination') !==null) {
        this.paginatedDiscussions.pagination = JSON.parse(response.headers.get('Pagination'))
      }
      return this.paginatedDiscussions;
    }))
  }

  loadDiscussion(id: number) {
    return this.http.get<Discussion>('https://localhost:5001/api/Discussions/details/' + id)
    .pipe(map(response => {
      return response
    }))
  }

  loadComments(commentParams: CommentParams, id: number) {
    let params = getPaginationHeaders(commentParams.pageNumber, commentParams.pageSize);

    return this.http.get<Comment[]>('https://localhost:5001/api/Discussions/comments/' + (id? id : ""), {observe: 'response', params})
    .pipe(map(response => {
      this.paginatedComments.result = response.body;
      if (response.headers.get('Pagination') !== null) {
          this.paginatedComments.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return this.paginatedComments;
    }));
  }

  createComment(body: string, commentId: number | null, discussionId: number, username: string, photoUrl: string, currentUserId: number): Observable<Comment> {
    return this.http.post<Comment>(
      "https://localhost:5001/api/Discussions/create-comment", 
        {
          body,
          commentId,
          appUserId: currentUserId,
          discussionId,
          username,
          photoUrl
        }
    )
  }

  updateComment(comment: Comment) {
    return this.http.put(
      "https://localhost:5001/api/Discussions/update-comment", comment)
  }


  deleteComment(id: number) {
    this.http.delete(
      "https://localhost:5001/api/Discussions/delete-comment/" + id).subscribe(
        data => {
          console.log(data);
        }
      )
  }

    setCommentParams(params: CommentParams) {
      this.commentParams = params;
  }

  resetCommentParams() {
    this.commentParams = new CommentParams;
    return this.commentParams;
  }
}

