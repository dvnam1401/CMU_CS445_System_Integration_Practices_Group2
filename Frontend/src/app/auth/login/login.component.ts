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
    this.model.email = this.model.email.trim();
    this.model.password = this.model.password.trim();
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
          // Kiểm tra roles
          if (!response.roles.includes('Read')) {  // Giả sử 'user' là role cần thiết
            alert('Bạn không có quyền truy cập vào khu vực này.');
            this.authService.logout();  // Đăng xuất người dùng
            this.router.navigateByUrl('/login');  // Chuyển hướng người dùng về trang đăng nhập
          } else {
            this.router.navigateByUrl('/');  // Chuyển hướng người dùng đến trang chủ
          }
          // this.router.navigateByUrl('/');
          // window.location.href = '/';
        },
        error: (error) => {
          console.error('Login failed', error);
          alert('Đăng nhập không thành công. Vui lòng thử lại.');
        }
      })
  }

  ngOnDestroy(): void {
    this.addCookie?.unsubscribe();
  }
}
