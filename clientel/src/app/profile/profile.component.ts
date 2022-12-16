import { Component, OnInit } from '@angular/core';
import { AdditionalInfo } from '../_models/additionalnfo';
import { User } from '../_models/User';
import { AccountService } from '../_services/account.service';
import { decodeUserId } from '../_services/tokenHelpers';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  constructor(private accountService: AccountService) {}

  user: User
  userId: number
  info: AdditionalInfo
  validationAdditionalErrors: string[] = []

  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('user'))
    this.userId = decodeUserId()
    this.accountService.getInfo(this.userId).subscribe(response =>
      this.info = response)
    console.log("user", this.user, "userId", this.userId, "info", this.info)
  }

  cancel() {
    console.log("cancel request received")
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
        this.info = model;
        console.log(response)
      },
      error: error => {
        console.log(error)
        this.validationAdditionalErrors = error;
    }
    })
  }
}
