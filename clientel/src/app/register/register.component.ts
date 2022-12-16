import { Component, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdditionalInfo } from '../_models/additionalnfo';
import { RegisterModel } from '../_models/registerModel';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';
import { decodeUserId } from '../_services/tokenHelpers';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  validationRegistrationErrors: string[] = [];
  validationAdditionalErrors: string[] = [];
  registrationSuccess: boolean = false;
  additionalInfoSuccess: boolean = false;


  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) { }

  @ViewChild('stepper') private stepper: MatStepper;

  ngOnInit(): void {

  }

  register( model: RegisterModel) {
    this.accountService.register(model).subscribe({
      next: response => {
        console.log(response);
        this.registrationSuccess = true;
        this.stepper.next();
      },
      error: error => {
        this.validationRegistrationErrors = error;
    },
  })
}

  cancel(stepper: MatStepper) {
    console.log("cancel request received")
    stepper.next();
    // this.router.navigateByUrl("/games");
  }

  addInfo( model: AdditionalInfo) {
    console.log("received event")
    let userId = decodeUserId()
    this.accountService.addInfo(userId, model).subscribe({
      next: response => {
        let user: User = JSON.parse(localStorage.getItem('user'))
        user.additionalInfo = true;
        this.accountService.setCurrentUser(user);
        console.log(response)
        this.additionalInfoSuccess = true;
        this.stepper.next();
      },
      error: error => {
        console.log(error)
        this.validationAdditionalErrors = error;
    }
    })
  }
}
