import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PersonalResponse } from '../models/personal-response.model';
import { PersonalEdit } from '../models/edit-personal.model';
import { BenefitPlan } from 'src/app/components/contents/models/benefit-plan.model';
import { BenefitPlanResponse } from '../models/benefit-request.model';
import { AddEmployee } from '../models/add-employee.model';
import { PayRate } from '../models/pay-rate.model';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {

  constructor(private http: HttpClient) { }

  getAllPersonal(): Observable<PersonalResponse[]> {
    return this.http.get<PersonalResponse[]>
      (`${environment.apiUrl}/api/Detail/getAll-personal`);
  }

  getAllPayRate(): Observable<PayRate[]> {
    return this.http.get<PayRate[]>
      (`${environment.apiUrl}/api/Detail/getAll-payRate`);
  }
  getPersonalById(id: number): Observable<PersonalResponse> {
    return this.http.get<PersonalResponse>
      (`${environment.apiUrl}/api/Detail/getPersonal-by-id?id=${id}`);
  }

  updatePersonal(id: number, personal: PersonalEdit): Observable<PersonalEdit> {
    return this.http.put<PersonalEdit>
      (`${environment.apiUrl}/api/Edit/edit-personal?idPersonal=${id}`, personal);
  }

  getAllBenefit(): Observable<BenefitPlanResponse[]> {
    return this.http.get<BenefitPlanResponse[]>
      (`${environment.apiUrl}/api/Detail/getAll-benefit`);
  }

  createPersonal(personal: PersonalEdit): Observable<PersonalEdit> {
    return this.http.post<PersonalEdit>
      (`${environment.apiUrl}/api/Created/createPersonal`, personal);
  }

  deletePersonal(id: number): Observable<HttpResponse<any>> {
    return this.http.delete<any>
      (`${environment.apiUrl}/api/Deleted/DeletePersonal/${id}`, { observe: 'response' });
  }

  deleteEmployee(idEmployment: number) : Observable<HttpResponse<any>> {
    return this.http.delete<any>
      (`${environment.apiUrl}/api/Deleted/DeleteEmployee/${idEmployment}`, { observe: 'response' });
  }

  addEmployee(employee: AddEmployee): Observable<AddEmployee> {
    return this.http.post<AddEmployee>
      (`${environment.apiUrl}/api/Created/createEmployee`, employee);
  }
}
