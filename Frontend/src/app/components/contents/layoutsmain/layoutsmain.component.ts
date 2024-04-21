import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { EmployeeFilter } from '../models/employee-filter.model';
import { EmployeeTotal } from '../models/employee-total-request.model';
import { EmployeeNotification } from '../models/employment-anniversary.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-layoutsmain',
  templateUrl: './layoutsmain.component.html',
  styleUrls: ['./layoutsmain.component.css'],
})
export class LayoutsmainComponent implements OnInit, OnDestroy{
  currentData?: EmployeeTotal[];
  employment$?: Observable<EmployeeNotification[]>;

  constructor(private employeeService: EmployeeService) { }
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {
    this.employment$ = this.employeeService.getAllNotification();
  }

  loadEarnings() {
    this.employeeService.getEmployeeSalary(this.onSubmit()).subscribe(data => {
      this.currentData = data;
    });
  }
  loadVacationDays() {
    this.employeeService.getEmployeeNumberVacation(this.onSubmit()).subscribe(data => {
      this.currentData = data;
    });
  }

  loadBenefits() {
    this.employeeService.getEmployeeAvergeBenefit(this.onSubmit()).subscribe(data => {
      this.currentData = data;
    });
  }

  onSubmit(): EmployeeFilter {
    return {
      isAscending: true,
      gender: null,
      category: null,
      ethnicity: null,
      department: null,
      year: null,
      month: null
    };
  }
}
