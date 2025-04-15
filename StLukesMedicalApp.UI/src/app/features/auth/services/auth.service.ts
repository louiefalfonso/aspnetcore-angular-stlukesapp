import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user.model';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user = new BehaviorSubject<User | undefined(undefined);

  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) { }


  // user
  user(): Observable<User | undefined>{
    return this.$user.asObservable();
  }
}
