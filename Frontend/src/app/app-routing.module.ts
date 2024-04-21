import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { EmployeeComponent } from './components/contents/employee/employee.component';
import { LayoutsmainComponent } from './components/contents/layoutsmain/layoutsmain.component';
import { NotificationComponent } from './components/contents/notification/notification.component';
import { AuthlayoutComponent } from './auth/authlayout/authlayout.component';
import { HomeComponent } from './home/home/home.component';

const routes: Routes = [
  {
    path: '',
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
        path: 'notification/anniversary',
        component: NotificationComponent,
        data: { type: 'anniversary', title: 'Number of Vacation Days' }

      },
      {
        path: 'notification/vacation',
        component: NotificationComponent,
        data: { type: 'vacation', title: 'Number of Vacation Days' }
      },
      {
        path: 'notification/benefit',
        component: NotificationComponent,
        data: { type: 'benefit', title: 'Number of Vacation Days' }
      },
      {
        path: 'notification/birthday',
        component: NotificationComponent,
        data: { type: 'birthday', title: 'Number of Vacation Days' }
      },
    ]
  },
  {
    path: '',
    component: AuthlayoutComponent,
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


