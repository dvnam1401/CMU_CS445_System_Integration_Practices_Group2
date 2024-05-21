import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserRequest } from '../models/user-request.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GroupPermission } from '../models/group-permission.model';
import { Dictionary } from '../models/dictionary.model';
import { UserUpdate } from '../models/user-update.model';
import { Groups } from '../models/group.model';
import { UserResponse } from '../models/user-response.model';
import { ResetPassword } from '../models/reset-password.model';
import { AddUser } from '../models/add-user.model';
import { AddGroup } from '../models/add-group.model';
import { Permission } from '../models/permission.model';
import { UpdatePermission } from '../models/edit-permission.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getAllUser(): Observable<UserRequest[]> {
    let users = this.http.get<UserRequest[]>
      (`${environment.apiUrl}/api/Admin/getAll-user`);
    console.log(users);
    return users;
  }

  findAccountUser(username: string): Observable<UserRequest[]> {
    return this.http.get<UserRequest[]>(
      `${environment.apiUrl}/api/Admin/find-account-user?search=${username}`);
  }

  getAllGroupPermission(): Observable<Dictionary<GroupPermission[]>> {
    return this.http.get<Dictionary<GroupPermission[]>>(`${environment.apiUrl}/api/Admin/get-all-permissions-by-group`);
  }

  getPermissionById(id: number): Observable<Dictionary<GroupPermission[]>> {
    return this.http.get<Dictionary<GroupPermission[]>>
      (`${environment.apiUrl}/api/Admin/get-permissions-userId?userId=${id}`);
  }

  getUserById(id: number): Observable<UserUpdate> {
    return this.http.get<UserUpdate>
      (`${environment.apiUrl}/api/Admin/getUser-by-id?id=${id}`);
  }

  getAllGroup(): Observable<Groups[]> {
    return this.http.get<Groups[]>
      (`${environment.apiUrl}/api/Admin/getAll-group`);
  }

  getAllPermission(): Observable<Permission[]> {
    return this.http.get<Permission[]>(`${environment.apiUrl}/api/Admin/getAll-permission`);
  }

  getPermissionUserById(userId: number, groupId: number[]): Observable<Dictionary<GroupPermission[]>> {
    return this.http.post<Dictionary<GroupPermission[]>>
      (`${environment.apiUrl}/api/Admin/get-group-user-by-id?userId=${userId}`, groupId);
  }

  updateUser(user: UserResponse): Observable<UserResponse> {
    return this.http.put<UserResponse>(
      `${environment.apiUrl}/api/Admin/update-user`, user);
  }

  updatePermissionUser(userId: number, groupPermission: GroupPermission[]): Observable<GroupPermission[]> {
    return this.http.put<GroupPermission[]>(
      `${environment.apiUrl}/api/Admin/update-permissions-status?userId=${userId}`, groupPermission);
  }

  updateGroupPermission(groupName: string, permission: UpdatePermission[]): Observable<UpdatePermission[]> {
    return this.http.put<UpdatePermission[]>(
      `${environment.apiUrl}/api/Admin/update-permission?groupName=${groupName}`, permission);
  }
  resetPasword(user: ResetPassword): Observable<ResetPassword> {
    return this.http.post<ResetPassword>(
      `${environment.apiUrl}/api/Admin/reset-password`, user);
  }

  createUser(user: AddUser): Observable<AddUser> {
    return this.http.post<AddUser>(
      `${environment.apiUrl}/api/Admin/create-AccountUser`, user);
  }

  createGroup(group: AddGroup): Observable<AddGroup> {
    return this.http.post<AddGroup>(
      `${environment.apiUrl}/api/Admin/create-GroupPermission`, group);
  }

  deleteUser(userId: number): Observable<HttpResponse<any>> {
    return this.http.delete<HttpResponse<any>>(
      `${environment.apiUrl}/api/Admin?userId=${userId}`, { observe: 'response' });
  }
}
