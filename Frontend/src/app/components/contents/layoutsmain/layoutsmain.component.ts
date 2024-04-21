import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { EmployeeFilter } from '../models/employee-filter.model';
import { EmployeeTotal } from '../models/employee-total-request.model';

@Component({
  selector: 'app-layoutsmain',
  templateUrl: './layoutsmain.component.html',
  styleUrls: ['./layoutsmain.component.css'],  
})
export class LayoutsmainComponent {
  currentData?: EmployeeTotal[];
  constructor(private employeeService: EmployeeService) { }

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
