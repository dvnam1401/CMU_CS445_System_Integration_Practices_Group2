import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { HeaderComponent } from './components/header/header.component';
import { LayoutsComponent } from './layouts/layouts.component';
import { EmployeeComponent } from './components/contents/employee/employee.component';
import { LayoutsmainComponent } from './components/layoutsmain/layoutsmain.component';
import { VacationComponent } from './components/contents/vacation/vacation.component';
import { BenefitsComponent } from './components/contents/benefits/benefits.component';
import { NotificationComponent } from './components/contents/notification/notification.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutsmainComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'layouts',
    component: LayoutsComponent
  },
  {
    path: 'employee',
    component: EmployeeComponent
  },
  {
    path: 'vacation',
    component: VacationComponent
  },
  {
    path: 'benefits',
    component: BenefitsComponent
  },
  {
    path: 'notification',
    component: NotificationComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}


