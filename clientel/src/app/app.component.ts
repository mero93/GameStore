import { HttpClient } from '@angular/common/http';
import { Component, OnInit, } from '@angular/core';
import { VERSION } from '@angular/material/core';
import { User } from './_models/User';
import { AccountService } from './_services/account.service';

// import { MatSelectSearchVersion } from 'ngx-mat-select-search';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'GameStore';

  version = VERSION;

  // matSelectSearchVersion = MatSelectSearchVersion;

  constructor(private http: HttpClient, private accountService: AccountService) {

  }
  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }
}
