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
import { LayoutsmainComponent } from './components/contents/layoutsmain/layoutsmain.component';
import { VacationComponent } from './components/contents/vacation/vacation.component';
import { NotificationComponent } from './components/contents/notification/notification.component';
import { HomeComponent } from './home/home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthLayoutComponent } from './auth/auth-layout/auth-layout.component';
import { EthnicitySelectorComponent } from './components/contents/ethnicity-selector/ethnicity-selector.component';
import { NotificationHiringComponent } from './components/contents/notification-hiring/notification-hiring.component';
import { AuthInterceptor } from './components/interceptors/auth.interceptor';
import { EditComponent } from './components/contents/edit/edit.component';
import { EditpayComponent } from './components/contents/editpay/editpay.component';
import { AddComponent } from './components/contents/add/add.component';
import { NotfoundComponent } from './components/contents/notfound/notfound.component';
import { LayoutNotfoundComponent } from './components/contents/layout-notfound/layout-notfound.component';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    EmployeeComponent,
    LoginComponent,
    LayoutsmainComponent,
    VacationComponent,
    NotificationComponent,
    HomeComponent,
    AuthLayoutComponent,
    EthnicitySelectorComponent,
    NotificationHiringComponent,
    EditComponent,
    EditpayComponent,
    NotfoundComponent,
    LayoutNotfoundComponent,
    AddComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [
    AuthLayoutComponent,
    HomeComponent,
  ]
})

export class AppModule { }
