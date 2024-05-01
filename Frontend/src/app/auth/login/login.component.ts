import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../service/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy {
  model: LoginRequest;
  private addCookie?: Subscription;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
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
          //set auth cookie
          this.cookieService.set('Authorization', `Bearer ${response.token}`, undefined, '/', undefined, true, 'Strict');
          //set user
          this.authService.setUser({
            email: response.email,
            roles: response.roles
          });
          this.router.navigateByUrl('/');
        }
      })
  }

  ngOnDestroy(): void {
    this.addCookie?.unsubscribe();
  }
}
