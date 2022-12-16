import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Discussion } from 'src/app/_models/discussion';
import { DiscussionsService } from 'src/app/_services/discussions.service';
import { decodeUserId } from 'src/app/_services/tokenHelpers';

@Component({
  selector: 'app-discussion-detail',
  templateUrl: './discussion-detail.component.html',
  styleUrls: ['./discussion-detail.component.css']
})
export class DiscussionDetailComponent implements OnInit{
  discussion: Discussion
  discussionLoaded = false
  currentUserId: number | null;
  username: string;
  photoUrl: string;
  isEditing: boolean = false
  discussionForm: FormGroup;

  canEdit: boolean = false;
  canDelete: boolean = false;

  constructor(private discussionService: DiscussionsService, private router: Router,
    private fb: FormBuilder) {}

  ngOnInit(): void {
    let url = this.router.url.split('/')
    let id = Number.parseInt(url[3])
    this.discussionService.loadDiscussion(id).subscribe( response => {
    this.discussion = response
    console.log(id)
    this.discussionLoaded = true
    let roles: string[];
    let user = JSON.parse(localStorage.getItem('user'))
    if (user !== null)
    {
      this.currentUserId = Number(decodeUserId());
      this.username = user['username'];
      this.photoUrl = user['photoUrl'];
      roles = user['roles']
      this.canEdit = this.currentUserId === this.discussion.appUserId;
      this.canDelete =
        (this.currentUserId === this.discussion.appUserId ||
          roles?.some(x => x === "ADMIN"))
    }
    this.discussionForm = this.fb.group({
      body: [this.discussion.body, Validators.required]
  });
   })
  }
  editingMode() {
    this.isEditing = !this.isEditing
  }

  updateDiscussion() {
    this.discussion.body = this.discussionForm.value.body
    this.editingMode()
  }
  cancel() {
    this.editingMode()
  }
  deleteDiscussion() {

  }
}
