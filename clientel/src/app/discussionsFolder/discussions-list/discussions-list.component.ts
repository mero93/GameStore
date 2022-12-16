import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Discussion } from 'src/app/_models/discussion';
import { DiscussionParams } from 'src/app/_models/DiscussionParams';
import { Pagination } from 'src/app/_models/pagination';
import { DiscussionsService } from 'src/app/_services/discussions.service';
import { decodeUserId } from 'src/app/_services/tokenHelpers';

@Component({
  selector: 'app-discussions-list',
  templateUrl: './discussions-list.component.html',
  styleUrls: ['./discussions-list.component.css']
})
export class DiscussionsListComponent implements OnInit {
  discussions: Discussion[] = []
  discussionParams: DiscussionParams = new DiscussionParams()
  pagination: Pagination;
  currentUserId: number | null;
  username: string;
  photoUrl: string;

  @Input() gameId: number | null = null


  constructor(private discussionsService: DiscussionsService, private router: Router) {}

  ngOnInit(): void {
    let item = localStorage.getItem('user');
    if (item !== 'null')
    {
      let user = JSON.parse(item);
      this.currentUserId = Number(decodeUserId());
      this.username = user['username'];
      this.photoUrl = user['photoUrl'];
    }
    
    if (this.gameId === null) {
      let url = this.router.url.split('/')
      console.log(url)
      this.gameId = Number.parseInt(url[2])
    }
    this.loadDiscussions()
    }

  loadDiscussions() {
    this.discussionsService.loadDiscussions(this.discussionParams, this.gameId).subscribe(
      response => {
        this.discussions = response.result;
        this.pagination = response.pagination;
      }
    );
  }

  pageChanged(event: any) {
    this.discussionParams.pageNumber = event.page;
    this.loadDiscussions();
  }
}
