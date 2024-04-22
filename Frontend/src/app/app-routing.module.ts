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

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent,
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
            component: NotificationComponent,
            data: { type: 'anniversary', title: 'Hiring anniversary' }
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
        path: 'edit',
        component: EditComponent
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

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}


