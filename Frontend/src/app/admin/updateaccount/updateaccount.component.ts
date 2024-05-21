import { UserUpdate } from './../models/user-update.model';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { GroupPermission } from '../models/group-permission.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, Subscription, switchMap } from 'rxjs';
import { AdminService } from '../services/admin.service';
import { Dictionary } from '../models/dictionary.model';
import { Groups } from '../models/group.model';
import { EmployeeService } from 'src/app/components/contents/services/employee.service';
import { UserResponse } from '../models/user-response.model';

@Component({
  selector: 'app-updateaccount',
  templateUrl: './updateaccount.component.html',
  styleUrls: ['./updateaccount.component.css']
})
export class UpdateaccountComponent implements OnInit, OnDestroy {
  id: number;
  modelGroup?: UserUpdate;
  modelPermission: GroupPermission[] = [];
  selectedGroup?: string[] = [];
  routeSubscription?: Subscription;
  updateSubscription?: Subscription;
  // permission$: Observable<Dictionary<GroupPermission[]>>;
  permission$: BehaviorSubject<Dictionary<GroupPermission[]>> = new BehaviorSubject({});
  groups$: Observable<Groups[]>;
  departments$?: Observable<string[]>;
  constructor(private route: ActivatedRoute,
    private adminService: AdminService,
    private employService: EmployeeService,
    private router: Router,) {
    this.departments$ = this.employService.getAllDepartment();
  }

  ngOnInit(): void {
    this.groups$ = this.adminService.getAllGroup();
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = +params.get('id')!;
        if (this.id) {
          this.adminService.getPermissionById(this.id).subscribe(
            (data: Dictionary<GroupPermission[]>) => {
              this.permission$.next(data);
            })
          this.adminService.getUserById(this.id).subscribe({
            next: (data) => {
              this.modelGroup = data;
              this.selectedGroup = data.groups.map(x => x.groupName);
              console.log(this.selectedGroup);
            }
          });
        }
      }
    })
  }

  getPermissionsById(selectedGroup: number[]): void {
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = +params.get('id')!;
        if (this.id) {
          this.adminService.getPermissionUserById(this.id, selectedGroup).subscribe(
            (data: Dictionary<GroupPermission[]>) => {
              this.permission$.next(data);
            })
        }
        //console.log(selectedGroup);
      }
    })
  }

  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  onSubmitForm(): void {
    console.log('Submit form:', this.modelGroup);
    if (this.modelGroup && this.id) {
      var updateUser: UserResponse = {
        userId: this.modelGroup.userId,
        userName: this.modelGroup.userName,
        firstName: this.modelGroup.firstName,
        lastName: this.modelGroup.lastName,
        email: this.modelGroup.email,
        phoneNumber: this.modelGroup.phoneNumber,
        isActive: this.modelGroup.isActive,
        department: this.modelGroup.department,
        groupIds: this.selectedGroup ? this.selectedGroup.filter(g => !isNaN(Number(g))).map(Number) : []
      }
      console.log(updateUser);
      this.adminService.updateUser(updateUser).subscribe({
        next: (data) => {
          this.router.navigateByUrl('/admin');
        }
      });
      console.log("Các quyền permission:", JSON.stringify(this.modelPermission));
      this.adminService.updatePermissionUser(this.id, this.modelPermission).subscribe({
        next: (data) => {
          //this.router.navigateByUrl('/admin');
        }
      });
    }
  }

  onCheckboxChange(event: any, permission: any, groupKey: string): void {
    const isChecked = event.target.checked;
    permission.isEnable = isChecked; // Cập nhật trạng thái isEnable dựa trên checkbox
    const updateIndex = this.modelPermission?.findIndex(pm =>
      pm.permissionId === permission.permissionId
      && pm.groupName === groupKey);
    if (updateIndex !== -1) {
      this.modelPermission[updateIndex].isEnable = isChecked;
    } else {
      this.modelPermission.push({
        permissionId: permission.permissionId,
        permissionName: permission.permissionName,
        groupName: permission.groupName,
        isEnable: isChecked,       
      });
    }
    console.log(`Permission ${permission.permissionName} in group ${groupKey} is now ${isChecked ? 'enabled' : 'disabled'}.`);
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
  }

}
