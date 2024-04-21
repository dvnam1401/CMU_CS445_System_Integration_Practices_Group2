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
import { BenefitsComponent } from './components/contents/benefits/benefits.component';
import { NotificationComponent } from './components/contents/notification/notification.component';
import { HomeComponent } from './home/home/home.component';
import { HttpClientModule } from '@angular/common/http';

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
    BenefitsComponent,
    NotificationComponent,
    HomeComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [
    AuthlayoutComponent,
    HomeComponent]
})

export class AppModule { }
