import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { EmployeeTotal } from '../models/employee-total-request.model';
import { EmployeeService } from '../services/employee.service';
import { EmployeeFilter } from '../models/employee-filter.model';

@Component({
  selector: 'app-vacation',
  templateUrl: './vacation.component.html',
  styleUrls: ['./vacation.component.css']
})
export class VacationComponent implements OnInit {
  employeeVacation$?: Observable<EmployeeTotal[]>;
  constructor(private service: EmployeeService) {
  }

  ngOnInit(): void {
    this.employeeVacation$ = this.service.getEmployeeNumberVacation(this.onSubmit());
    console.log(this.employeeVacation$.forEach((x) => console.log(x)));
  }

  onSubmit(): EmployeeFilter {
    return {
      isAscending: null,
      gender: "female",
      category: null,
      ethnicity: null,
      department: null,
      year: null,
      month: null
    };
  }

}
