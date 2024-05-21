import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription, Observable } from 'rxjs';
import { AddUser } from '../models/add-user.model';
import { Groups } from '../models/group.model';
import { AdminService } from '../services/admin.service';
import { EmployeeService } from 'src/app/components/contents/services/employee.service';

@Component({
  selector: 'app-taoaccount',
  templateUrl: './taoaccount.component.html',
  styleUrls: ['./taoaccount.component.css']
})
export class TaoaccountComponent implements OnInit, OnDestroy {
  model: AddUser;
  private addUserSubscription?: Subscription;
  groupId$: Observable<Groups[]>;
  departments$?: Observable<string[]>;
  constructor(private router: Router,
    private adminService: AdminService,
    private employService: EmployeeService,) {
    this.model = {
      firstName: '',
      lastName: '',
      userName: '',
      email: '',
      password: '',
      phoneNumber: '',
      department: '',
      isActive: true,
      groupIds: []
    };
  }

  ngOnInit(): void {
    this.departments$ = this.employService.getAllDepartment();
    this.groupId$ = this.adminService.getAllGroup();   
  }
  onSubmitForm(): void {
    console.log(this.model);
    this.addUserSubscription = this.adminService.createUser(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin');
        }
      })
  }
  ngOnDestroy(): void {
    this.addUserSubscription?.unsubscribe();
  }
}
