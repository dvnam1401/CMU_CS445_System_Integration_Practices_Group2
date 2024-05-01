import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { EmployeeComponent } from './components/contents/employee/employee.component';
import { LayoutsmainComponent } from './components/contents/layoutsmain/layoutsmain.component';
import { NotificationComponent } from './components/contents/notification/notification.component';
import { HomeComponent } from './home/home/home.component';
import { AuthLayoutComponent } from './auth/auth-layout/auth-layout.component';
import { EditComponent } from './components/contents/edit/edit.component';
import { EditpayComponent } from './components/contents/editpay/editpay.component';
import { AddComponent } from './components/contents/add/add.component';
import { NotificationHiringComponent } from './components/contents/notification-hiring/notification-hiring.component';
import { authGuard } from './auth/guards/auth.guard';
import { roleGuard } from './components/guards/role.guard';
import { LayoutNotfoundComponent } from './components/contents/layout-notfound/layout-notfound.component';
import { NotfoundComponent } from './components/contents/notfound/notfound.component';

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
      {
        path: 'edit-hr',
        component: EditComponent,
        canActivate: [roleGuard],
      },
      {
        path: 'edit-payroll',
        component: EditpayComponent,
        canActivate: [roleGuard],
      },
      {
        path: 'add',
        component: AddComponent,
        canActivate: [roleGuard],
      }
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
    path: '',
    component: LayoutNotfoundComponent,
    children: [
      {
        path: '**',
        component: NotfoundComponent
      },]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}