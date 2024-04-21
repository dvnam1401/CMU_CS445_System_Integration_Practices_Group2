import { Injectable } from '@angular/core';
import { Observable, catchError, tap, throwError } from 'rxjs';
import { EmployeeTotal } from '../models/employee-total-request.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { EmployeeFilter } from '../models/employee-filter.model';
import { EmployeeNotification } from '../models/employment-anniversary.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  getEmployeeSalary(filter: EmployeeFilter): Observable<EmployeeTotal[]> {
    return this.http.post<EmployeeTotal[]>(`${environment.apiUrl}/api/Services/filter/employees`, filter);
  }

  getEmployeeNumberVacation(filter: EmployeeFilter): Observable<EmployeeTotal[]> {
    return this.http.post<EmployeeTotal[]>(`${environment.apiUrl}/api/Services/filter/number-vacation-days`, filter);
  }  

  getEmployeeAvergeBenefit(filter: EmployeeFilter): Observable<EmployeeTotal[]> {
    return this.http.post<EmployeeTotal[]>(`${environment.apiUrl}/api/Services/filter/average-benefit`, filter);
  }

  getEmploymentAnniversary(): Observable<EmployeeNotification[]> {
    return this.http.get<EmployeeNotification[]>(`${environment.apiUrl}/api/Services/GetEmployeeAnniversary`);
  }

  getEmployeeBirthday(): Observable<EmployeeNotification[]> {
    return this.http.get<EmployeeNotification[]>(`${environment.apiUrl}/api/Services/GetEmployeeBirthday`);
  }
  getVacationEmployeeThisYear(): Observable<EmployeeNotification[]> {
    return this.http.get<EmployeeNotification[]>(`${environment.apiUrl}/api/Services/CountVacationEmployee`);
  }
}
