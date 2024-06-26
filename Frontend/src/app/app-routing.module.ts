import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { EmployeeComponent } from './components/contents/employee/employee.component';
import { LayoutsmainComponent } from './components/contents/layoutsmain/layoutsmain.component';
import { NotificationComponent } from './components/contents/notification/notification.component';
import { HomeComponent } from './home/home/home.component';
import { AuthLayoutComponent } from './auth/auth-layout/auth-layout.component';
import { NotificationHiringComponent } from './components/contents/notification-hiring/notification-hiring.component';
import { authGuard } from './auth/guards/auth.guard';
import { LayoutNotfoundComponent } from './components/contents/layout-notfound/layout-notfound.component';
import { NotfoundComponent } from './components/contents/notfound/notfound.component';
import { ManageformComponent } from './manager/manageform/manageform.component';
import { LayoutmanageComponent } from './manager/layoutmanage/layoutmanage.component';

import { TaoemployeeComponent } from './manager/taoemployee/taoemployee.component';


import { PageAdminComponent } from './admin/page-admin/page-admin.component';
import { TaoaccountComponent } from './admin/taoaccount/taoaccount.component';
import { MainadmminComponent } from './admin/mainadmmin/mainadmmin.component';
import { UpdateaccountComponent } from './admin/updateaccount/updateaccount.component';
import { TaogroupComponent } from './admin/taogroup/taogroup.component';
import { HosonhanvienComponent } from './manager/hosonhanvien/hosonhanvien.component';
import { ManageemployeeComponent } from './manager/manageemployee/manageemployee.component';
import { DangkigroupComponent } from './admin/dangkigroup/dangkigroup.component';
import { XemthongtinchitietComponent } from './manager/xemthongtinchitiet/xemthongtinchitiet.component';
import { UpdateemployeeComponent } from './manager/updateemployee/updateemployee.component';


const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        component: LayoutsmainComponent
      },
      {
        path: 'employee',
        component: EmployeeComponent,
        data: { type: 'totalEarnings', title: 'Total Earnings' }
      },
      {
        path: 'vacation',
        component: EmployeeComponent,
        data: { type: 'vacationDays', title: 'Number of Vacation Days' }
      },
      {
        path: 'benefits',
        component: EmployeeComponent,
        data: { type: 'averageBenefits', title: 'Average Benefits' }
      },
      {
        path: 'notification',
        children: [
          {
            path: 'anniversary',
            component: NotificationHiringComponent,
            // data: { type: 'anniversary', title: 'Hiring anniversary' }
          },
          {
            path: 'vacation',
            component: NotificationComponent,
            data: { type: 'vacation', title: 'Notifications of vacation' }
          },
          {
            path: 'benefit',
            component: NotificationComponent,
            data: { type: 'benefit', title: 'Change benefits plan ' }
          },
          {
            path: 'birthday',
            component: NotificationComponent,
            data: { type: 'birthday', title: 'Birthdays' }
          },
        ]
      },
    ]
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: 'login',
        component: LoginComponent
      },
    ]
  },
  {
    path: 'manage',
    component: LayoutmanageComponent,
    canActivate: [authGuard],
    children: [
      {
        path: 'personal',
        component: ManageformComponent
      },
      {
        path: 'employee',
        component: ManageemployeeComponent,
      },
      {
        path: 'add-personal',
        component: HosonhanvienComponent,
      },
      {
        path: 'edit-employee/:id',
        component: UpdateemployeeComponent,
      },
      {
        path: 'add-employee/:id',
        component: TaoemployeeComponent,
      },
      {
        path: 'edit-personal/:id',
        component: HosonhanvienComponent,
      },
      {
        path: 'thongtinchitiet-employee/:id',
        component: XemthongtinchitietComponent,
      },
    ]
  },
  {
    path: 'admin',
    component: PageAdminComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        component: MainadmminComponent
      },
      {
        path: 'add-user',
        component: TaoaccountComponent
      },
      {
        path: 'manage-group',
        component: TaogroupComponent
      },
      {
        path: 'edit-user/:id',
        component: UpdateaccountComponent
      },
      {
        path: 'dangkigroup',
        component: DangkigroupComponent
      },
    ]
  },

  {
    path: '',
    component: LayoutNotfoundComponent,
    children: [
      {
        path: '**',
        component: NotfoundComponent
      },]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}