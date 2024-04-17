import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { EmployeeComponent } from './components/contents/employee/employee.component';
import { LoginComponent } from './auth/login/login.component';
import { LayoutsComponent } from './layouts/layouts.component';
import { LayoutsmainComponent } from './components/layoutsmain/layoutsmain.component';
import { VacationComponent } from './components/contents/vacation/vacation.component';
import { BenefitsComponent } from './components/contents/benefits/benefits.component';
import { NotificationComponent } from './components/contents/notification/notification.component';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    EmployeeComponent,
    LoginComponent,
    LayoutsComponent,
    LayoutsmainComponent,
    VacationComponent,
    BenefitsComponent,
    NotificationComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
  ],
  // providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }
