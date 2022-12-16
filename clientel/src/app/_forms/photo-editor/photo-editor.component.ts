import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import {FileUploader} from 'ng2-file-upload'
import { Observable, Subscription } from 'rxjs';
import { Game } from '../../_models/Game';
import { ImageUplModelType } from '../../_models/imageUplModelType';
import { Photo } from '../../_models/photo';
import { User } from '../../_models/User';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit, OnDestroy {
  constructor(private http: HttpClient) {}

  @Input() model: any
  @Input() imageUplModelType: ImageUplModelType
  @Input() events: Observable<void>;

  @Output() handleImageUploaded = new EventEmitter<Photo | void>();

  private eventsSubscription: Subscription;
  url: string
  token: string;
  uploader: FileUploader;
  hasBaseDropzoneOver = false;

  ngOnInit(): void {
    this.eventsSubscription = this.events.subscribe(() => this.uploadImages());
    this.token = JSON.parse(localStorage.getItem('user')).token;
    this.defineUrl();
    this.initializeUploader();
  }

  ngOnDestroy() {
    this.eventsSubscription.unsubscribe();
  }

  fileOverBase(e: any) {
     this.hasBaseDropzoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.url,
      authToken: 'Bearer ' + this.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const image : Photo = JSON.parse(response);

        console.log("event with photo fired");
        this.handleImageUploaded.emit(image);
      }
    }
  }

  defineUrl() {
    if ((this.model) && this.imageUplModelType === ImageUplModelType.User) {
      this.url = 'https://localhost:5001/api/photos/user/add-photo/' + this.model.username
    }

    else if (this.imageUplModelType === ImageUplModelType.Game) {
      if (this.model) {
        this.url = 'https://localhost:5001/api/photos/game/add-photo/' + this.model.id
      }
      else {
        this.url = 'https://localhost:5001/api/photos/game/add-photo/0'
      }
    }

    else {
      throw new Error("Can't define type of input object")
    }
  }

  uploadImages(): void {
    console.log("photo editor received event")
    if (this.uploader.queue.length > 0) {
      this.uploader.uploadAll();
    }
    else {
      console.log("event without photo fired");
      this.handleImageUploaded.emit();
    }
  }
}



