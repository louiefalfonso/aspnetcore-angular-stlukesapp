import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { LoginsRequest } from '../models/login-request.models';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  model: LoginsRequest;

  constructor(
    private router : Router,
    private authService : AuthService,
    private cookieService : CookieService
  ){
    this.model = {
      email: '',
      password: ''
    };
  }

  // implement form submit
  onFormSubmit(): void {
    this.authService.login(this.model)
    .subscribe({
      next: (response) => {

        // set auth cookie
        this.cookieService.set('Authorization', `Bearer ${response.token}`,
          undefined, '/' , undefined, true, 'Strict');

        // set user
        this.authService.setUser({
          email: response.email,
          roles: response.roles
        });
        
        // redirect back to home page
        this.router.navigateByUrl('/');
      },
      error: (error) => {}
    })
  }

}
