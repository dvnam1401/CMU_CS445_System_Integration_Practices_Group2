import { Component, OnInit } from '@angular/core';
import { GroupPermission } from '../models/group-permission.model';
import { Observable, map } from 'rxjs';
import { AdminService } from '../services/admin.service';
import { Dictionary } from '../models/dictionary.model';
import { GroupUser } from '../models/group-user.model';
import { GroupPermissions } from '../models/dictionary-permission.model';
import { UpdatePermission } from '../models/edit-permission.model';

@Component({
  selector: 'app-taogroup',
  templateUrl: './taogroup.component.html',
  styleUrls: ['./taogroup.component.css']
})
export class TaogroupComponent implements OnInit {
  groupPermission$?: Observable<{ [key: string]: GroupPermission[] }>;
  updates: UpdatePermission[] = [];
  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.groupPermission$ = this.adminService.getAllGroupPermission();
    console.log(this.groupPermission$.forEach(x => console.log(x)));
  }
  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  getFormattedUserNames(users: GroupUser[]): string {
    return users.map(user => user.userName).join(', ');
  }

  onCheckboxChange(event: any, permission: any, groupKey: string): void {
    const isChecked = event.target.checked;
    permission.isEnable = isChecked; // Cập nhật trạng thái isEnable dựa trên checkbox
    const updateIndex = this.updates?.findIndex(pm =>
      pm.permissionId === permission.permissionId
    );
    if (updateIndex !== -1) {
      this.updates[updateIndex].isEnable = isChecked;
    } else {
      this.updates.push({
        permissionId: permission.permissionId,
        permissionName: permission.permissionName,
        isEnable: isChecked,
      });
    }
    console.log(`Permission ${permission.permissionName} in group ${groupKey} is now ${isChecked ? 'enabled' : 'disabled'}.`);
  }

  updateGroupPermissions(groupName: string): void {
    if (this.updates.length > 0) {
      this.adminService.updateGroupPermission(groupName, this.updates).subscribe({
        next: response => {
          alert('Permissions updated successfully');
          // Optionally refresh data or clear updates
        },
        error: error => alert('Failed to update permissions: ' + error.message)
      });
    }
  }
}
