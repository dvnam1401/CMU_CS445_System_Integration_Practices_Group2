import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/auth/models/user.models';
import { AuthService } from 'src/app/auth/service/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  user?: User;
  constructor(private authService: AuthService, private router: Router) {
  }

  ngOnInit(): void {
    this.authService.user()
      .subscribe({
        next: (response) => {
          this.user = response;
        }
      });
    this.user = this.authService.getUser();
    console.log(this.user);
  }
  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/login');
  }
}
