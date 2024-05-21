import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { EmployeeTotal } from '../models/employee-total-request.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { EmployeeFilter } from '../models/employee-filter.model';
import { EmployeeNotification } from '../models/employment-anniversary.model';
import { Employment } from '../models/update-employment.model';
import { EmploymentPayRoll } from '../models/update-employment-payroll.model';
import { BenefitPlan } from '../models/benefit-plan.model';
import { PayRate } from '../models/pay-rate.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient
  ) { }

  getAllDepartment(): Observable<string[]> {
    return this.http.get<string[]>
      (`${environment.apiUrl}/api/Summarized/getAllDepartement?addAuth=true`);
  }

  getEmploymentById(id: number): Observable<Employment> {
    return this.http.get<Employment>
      (`${environment.apiUrl}/api/HR/${id}?addAuth=true`);
  }

  // updateEmploymentHr(id: number, updateEmployment: Employment): Observable<Employment> {
  //   return this.http.put<Employment>(
  //     `${environment.apiUrl}/api/HR/${id}?addAuth=true`, updateEmployment);
  // }

  getEmploymentByIdPayRoll(id: number): Observable<EmploymentPayRoll> {
    return this.http.get<EmploymentPayRoll>
      (`${environment.apiUrl}/api/Payroll/${id}?addAuth=true`);
  }

  // updateEmploymentPayRoll(id: number, updateEmployment: EmploymentPayRoll): Observable<EmploymentPayRoll> {
  //   return this.http.put<EmploymentPayRoll>(
  //     `${environment.apiUrl}/api/Payroll/${id}?addAuth=true`, updateEmployment);
  // }

  getBenefitPlan(): Observable<BenefitPlan[]> {
    return this.http.get<BenefitPlan[]>
      (`${environment.apiUrl}/api/HR/getBenefit`);
  }

  // createEmployment(data: AddEmployment): Observable<AddEmployment> {
  //   console.log('Sending data:', data);
  //   return this.http.post<AddEmployment>(
  //     `${environment.apiUrl}/api/HR/createEmployment`, data);
  // }

  getAllPayRate(): Observable<PayRate[]> {
    return this.http.get<PayRate[]>
      (`${environment.apiUrl}/api/Payroll`);
  }
// summarized
  getEmployeeSalary(filter: EmployeeFilter): Observable<EmployeeTotal[]> {
    return this.http.post<EmployeeTotal[]>
      (`${environment.apiUrl}/api/Summarized/filter/employees?addAuth=true`, filter);
  }
  
  getEmployeeNumberVacation(filter: EmployeeFilter): Observable<EmployeeTotal[]> {
    return this.http.post<EmployeeTotal[]>
      (`${environment.apiUrl}/api/Summarized/filter/number-vacation-days?addAuth=true`, filter);
  }

  getEmployeeAvergeBenefit(filter: EmployeeFilter): Observable<EmployeeTotal[]> {
    return this.http.post<EmployeeTotal[]>
      (`${environment.apiUrl}/api/Summarized/filter/average-benefit?addAuth=true`, filter);
  }

  // notification
  getEmploymentAnniversary(daysLimit: number): Observable<EmployeeNotification[]> {
    // let params = new HttpParams().set('daysLimit', daysLimit);
    return this.http.get<EmployeeNotification[]>
      (`${environment.apiUrl}/api/Notification/GetEmployeeAnniversary/?daysLimit=${daysLimit}`);
  }

  getEmployeeBirthday(): Observable<EmployeeNotification[]> {
    return this.http.get<EmployeeNotification[]>
      (`${environment.apiUrl}/api/Notification/GetEmployeeBirthday?addAuth=true`);
  }

  getVacationEmployeeThisYear(): Observable<EmployeeNotification[]> {
    return this.http.get<EmployeeNotification[]>
      (`${environment.apiUrl}/api/Notification/CountVacationEmployee?addAuth=true`);
  }

  getAllNotification(): Observable<EmployeeNotification[]> {
    return this.http.get<EmployeeNotification[]>
      (`${environment.apiUrl}/api/Notification/getAllVacation?addAuth=true`);
  }
}
