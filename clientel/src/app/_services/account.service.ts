import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable, ReplaySubject } from 'rxjs';
import { AdditionalInfo } from '../_models/additionalnfo';
import { AuthorizationModel } from '../_models/authorizationModel';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource;

  constructor(private http: HttpClient, private router: Router) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'auth/login-user', model).pipe(
      map((response: User) => {
        const user = response
        if (user) {
          this.setCurrentUser(user);
        }
        if (this.router.url === '/register') {
          this.router.navigateByUrl('/games')
        }
        window.location.reload();
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'auth/register-user', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  addInfo(id: number, model: AdditionalInfo){
    return this.http.put<AdditionalInfo>(this.baseUrl + 'auth/add-info/' + id, model)  
  }

  getInfo(id: number): Observable<AdditionalInfo>{
    return this.http.get<AdditionalInfo>(this.baseUrl + 'auth/get-info/' + id)    
  }

  refreshToken(user: User) {
    return this.http.post(this.baseUrl + 'auth/refresh-token', user)
    .pipe(map((auth: AuthorizationModel) => {
      user.expiresAt = auth.expiresAt;
      user.token = auth.token;
      this.setCurrentUser(user);
    }))
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('order');
    this.currentUserSource.next(null)
    window.location.reload();
  }
}


