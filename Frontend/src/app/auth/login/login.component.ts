import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequest } from '../models/login-request.model';
import { Subscription } from 'rxjs';
import { AuthService } from '../services/auth.service';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: LoginRequest;
  // private addCookie?: Subscription;
  constructor(private authService: AuthService,
    // private cookieService: CookieService,
    // private router: Router
  ) {
    this.model = {
      email: '',
      password: ''
    };
  }

  onFormSubmit(): void {
    this.authService.login(this.model)
      .subscribe({
        next: (response) => {
          console.log(response);
        }
      })
  }
}
