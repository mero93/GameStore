import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { AdditionalInfo } from 'src/app/_models/additionalnfo';
import { ImageUplModelType } from 'src/app/_models/imageUplModelType';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/User';
import { __values } from 'tslib';

const PAYMENTOPTIONS: string[] = [
  "PayPal",
  "MasterCard",
  "Visa",
  "Venmo",
  "Apple Pay",
  "Google Pay"
]

@Component({
  selector: 'app-additional-info-form',
  templateUrl: './additional-info-form.component.html',
  styleUrls: ['./additional-info-form.component.css']
})
export class AdditionalInfoFormComponent  implements OnInit {

  form: FormGroup;
  namePattern = "^[a-zA-Z]*$";
  phonePattern = "^[0-9]*$";
  validationErrors: string[] = [];
  model: AdditionalInfo;
  options = PAYMENTOPTIONS
  user: User

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    console.log("initiate additional-info")
    this.user = JSON.parse(localStorage.getItem('user'))
    this.initializeForm();
  }

  @Input() hideCancel = false;

  @Output()
  handleSubmit = new EventEmitter<AdditionalInfo>();

  @Output()
  handleCancel = new EventEmitter<void>();



  initializeForm() {
    this.form = this.fb.group({
      firstName: [ this.model? this.model.firstName : "", 
        [Validators.required, Validators.maxLength(20), Validators.pattern(this.namePattern)]],
      lastName: [ this.model? this.model.lastName : "",
        [Validators.required, Validators.maxLength(20), Validators.pattern(this.namePattern)]],
      phone: [ this.model? this.model.phone : "",
        [Validators.required, Validators.maxLength(9), Validators.minLength(9),
        Validators.pattern(this.phonePattern)]],
      paymentType: [ this.model? this.model.paymentType : "", [Validators.required]],
      comment: [ this.model? this.model.comment : "", [Validators.maxLength(600)]]
    })
  }

  onSubmit() {
    let info = new AdditionalInfo(this.form.value)
    this.handleSubmit.emit(info);
    console.log('info event fired', info);
    this.form.reset();
  }

  onCancel() {
    this.handleCancel.emit();
    console.log('info cancel fired');
    this.form.reset();
  }
}
