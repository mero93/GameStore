import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { RegisterModel } from 'src/app/_models/registerModel';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {

  form: FormGroup;
  unamePattern = "^[a-zA-Z0-9_-]*$";
  passwordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{1,}$";

  //(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*#?&])
  @Input() validationErrors: string[] = [];
  model: RegisterModel;



  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  @Output()
  handleSubmit = new EventEmitter<string>();

  @Output()
  handleCancel = new EventEmitter<void>();



  initializeForm() {
    this.form = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(12), Validators.pattern(this.unamePattern)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(16), Validators.pattern(this.passwordPattern)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    })
    this.form.controls.password.valueChanges.subscribe(() => {
    this.form.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value ?
      null : {isMatching: true}
    }
  }

  onSubmit() {
    console.log('onsubmit', this.form.value);
    this.handleSubmit.emit(this.form.value);
    this.form.reset();
  }

  onCancel() {
    this.handleCancel.emit();
    this.form.reset();
  }
}
