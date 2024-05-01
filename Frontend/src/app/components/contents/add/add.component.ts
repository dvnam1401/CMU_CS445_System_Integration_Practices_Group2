import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddEmployment } from '../models/add-employee.model';
import { EmployeeService } from '../services/employee.service';
import { Observable, Subscription } from 'rxjs';
import { BenefitPlan } from '../models/benefit-plan.model';
import { Router } from '@angular/router';
import { PayRate } from '../models/pay-rate.model';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit, OnDestroy {
  model: AddEmployment;
  benefitPlan$?: Observable<BenefitPlan[]>;
  departments$?: Observable<string[]>;
  payRates$?: Observable<PayRate[]>;
  private addEmployeeSubscription?: Subscription;

  constructor(private employeeService: EmployeeService,
    private router: Router,
  ) {
    this.model = {
      personalId: 0,
      benefitPlanId: 0,
      employmentId: 0,
      lastName: '',
      firstName: '',
      payRatesIdPayRates: 0,
      phoneNumber: '',
      ssn: 0,
      shareholderStatus: 0,
      email: '',
      address: '',
      gender: '',
      department: '',
    }
  }
  ngOnInit(): void {
    console.log("Initializing AddComponent");
    this.loadInitialData();
  }

  loadInitialData() {
    this.benefitPlan$ = this.employeeService.getBenefitPlan();
    this.departments$ = this.employeeService.getAllDepartment();
    this.departments$.subscribe(data => console.log('Departments Loaded:', data), error => console.error('Failed to load departments:', error));

    this.payRates$ = this.employeeService.getAllPayRate();
    this.payRates$.subscribe(data => console.log('Pay Rates Loaded:', data), error => console.error('Failed to load pay rates:', error));
  }

  onFormSubmit(): void {
    console.log("Final model data:", this.model);
    this.addEmployeeSubscription = this.employeeService.createEmployment(this.model)
      .subscribe({
        next: (response) => {
          console.log(this.model);
          alert('Update successful');
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigateByUrl('/add');
          });
        }
      });
  }

  ngOnDestroy(): void {
    this.addEmployeeSubscription?.unsubscribe();
  }
}
