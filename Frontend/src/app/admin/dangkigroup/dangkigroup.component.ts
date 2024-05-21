import { Component, OnDestroy, OnInit } from '@angular/core';
import { Groups } from '../models/group.model';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AdminService } from '../services/admin.service';
import { AddGroup } from '../models/add-group.model';
import { Permission } from '../models/permission.model';

@Component({
  selector: 'app-dangkigroup',
  templateUrl: './dangkigroup.component.html',
  styleUrls: ['./dangkigroup.component.css']
})
export class DangkigroupComponent implements OnInit, OnDestroy {
  permission$: Observable<Permission[]>;
  private addGroupSubscription?: Subscription;
  model: AddGroup;
  constructor(private router: Router,
    private adminService: AdminService
  ) {
    this.model = {
      groupName: '',
      permissionIds: [],
    };
  }
  

  ngOnInit(): void {
    this.permission$ = this.adminService.getAllPermission();
  }

  onSubmitForm(): void {
      this.addGroupSubscription = this.adminService.createGroup(this.model).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/manage-group');
        }
      })
  }

  ngOnDestroy(): void {
   this.addGroupSubscription?.unsubscribe();
  }
}
