import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/auth/models/user.models';
import { AuthService } from 'src/app/auth/service/auth.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit{
  roles: string[];
  user?: User;
  constructor(private authService: AuthService, private router: Router) {}
  ngOnInit(): void {
    this.authService.user()
      .subscribe({
        next: (response) => {
          this.user = response;
        }
      });
    this.user = this.authService.getUser();
    this.roles = this.authService.getUserRoles();    
  }

  onEditClicked(event: Event) {
    event.preventDefault();
    console.log(this.roles);
    if (this.roles.includes('HR')) {
      this.router.navigate(['/edit-hr']);
    } else if (this.roles.includes('PayRoll')) {
      this.router.navigate(['/edit-payroll']);
    } else {
      this.router.navigate(['/login']);
    }
  }  
}
