import { Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { EmployeeTotal } from '../models/employee-total-request.model';
import { Observable, Subscription, catchError, switchMap } from 'rxjs';
import { EmployeeService } from '../services/employee.service';
import { EmployeeFilter } from '../models/employee-filter.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  employeeSalary$?: Observable<EmployeeTotal[]>;
  title?: string;
  // objectKeys = Object.keys;

  constructor(private service: EmployeeService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    // Directly map route data to the employeeSalary$ observable
    this.employeeSalary$ = this.route.data.pipe(
      switchMap(data => {
        const type = data['type'];
        return this.fetchDataBasedOnType(type);
      }),
      catchError(error => {
        // Handle errors or provide a fallback value
        console.error('Error fetching data:', error);
        return []; // Return an empty array or use of([]) if needed
      })
    );
    this.route.data.subscribe(data => {
      this.title = data['title'];
      this.employeeSalary$ = this.fetchDataBasedOnType(data['type']);
    });
  }

  fetchDataBasedOnType(type: string): Observable<EmployeeTotal[]> {
    const filter = this.onSubmit();
    switch (type) {
      case 'totalEarnings':
        return this.service.getEmployeeSalary(filter);
      case 'vacationDays':
        return this.service.getEmployeeNumberVacation(filter);
      case 'averageBenefits':
        return this.service.getEmployeeAvergeBenefit(filter);
      default:
        return this.service.getEmployeeSalary(filter);
    }
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
