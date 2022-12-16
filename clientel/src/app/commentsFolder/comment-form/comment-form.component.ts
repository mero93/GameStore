import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-comment-form',
  templateUrl: './comment-form.component.html',
  styleUrls: ['./comment-form.component.css']
})
export class CommentFormComponent implements OnInit {
 @Input() hasCancelButton: boolean = false;
 @Input() initialText: string = '';
 @Input() submitLabel: string

  @Output()
  handleSubmit = new EventEmitter<string>();

  @Output()
  handleCancel = new EventEmitter<void>();

 commentForm: FormGroup;


 constructor(private fb: FormBuilder) {}
  ngOnInit(): void {
    this.commentForm = this.fb.group({
        body: [this.initialText, Validators.required]
    });
  }

  onSubmit() {
    console.log('onsubmit', this.commentForm.value)
    this.handleSubmit.emit(this.commentForm.value.body);
    this.commentForm.reset()
  }
}
