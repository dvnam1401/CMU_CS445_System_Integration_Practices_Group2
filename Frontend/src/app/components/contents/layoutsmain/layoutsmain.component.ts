import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { EmployeeFilter } from '../models/employee-filter.model';
import { EmployeeTotal } from '../models/employee-total-request.model';

@Component({
  selector: 'app-layoutsmain',
  templateUrl: './layoutsmain.component.html',
  styleUrls: ['./layoutsmain.component.css']
})
export class LayoutsmainComponent implements OnInit {
  currentData?: EmployeeTotal[];
  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    throw new Error('Method not implemented.');
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

  // loadBenefits() {
  //   this.employeeService.getEmployeeBenefits().subscribe(data => {
  //     this.currentData = data;
  //     this.showTable = true;
  //   });
  // }

  onSubmit(): EmployeeFilter {
    return {
      isAscending: null,
      gender: null,
      category: null,
      ethnicity: null,
      department: null,
      year: null,
      month: null
    };
  }
}
