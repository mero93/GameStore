import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/User';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  isRefreshing: boolean = false

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User;
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
    if (currentUser) {
      request = request.clone({
        setHeaders: {
          Authorization: 'Bearer ' + currentUser.token,
        }
      })
      
      if (!this.isRefreshing)
      {
        this.isRefreshing = true;
        if (new Date(currentUser.expiresAt).getTime() < new Date().getTime())
        {
          this.accountService.refreshToken(currentUser).subscribe()
          console.log('jwt interceptor token renewed')
        }
      }
  
      this.isRefreshing = false;
    }



    return next.handle(request);
  }
}
