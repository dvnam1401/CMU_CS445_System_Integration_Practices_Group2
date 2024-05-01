import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { EmployeeService } from '../services/employee.service';
import { EmploymentPayRoll } from '../models/update-employment-payroll.model';
import { PayRate } from '../models/pay-rate.model';

@Component({
  selector: 'app-editpay',
  templateUrl: './editpay.component.html',
  styleUrls: ['./editpay.component.css']
})
export class EditpayComponent {
  id: string | null = null;
  model?: EmploymentPayRoll;
  routeSubscription?: Subscription;
  updateEmploymentSubscription?: Subscription;
  payRates$?: Observable<PayRate[]>;
  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private router: Router,

  ) {
  }

  ngOnInit(): void {
    this.payRates$ = this.employeeService.getAllPayRate();
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (param) => {
        this.id = param.get('id');

        if (this.id) {
          this.findEmployee();
        }
      }
    })
  }
  findEmployee(): void {
    console.log(`Get employee with id: ${this.id}`);
    if (this.id) {
      this.employeeService.getEmploymentByIdPayRoll(parseInt(this.id)).subscribe({
        next: (response) => {
          this.model = response;
          console.log(this.model);
        },
        error: err => {
          console.error('Failed to fetch employee:', err);
        }
      });
    }
  }

  onFormSubmit(): void {
    if (this.model && this.id) {
      var update: EmploymentPayRoll = {
        employmentId: this.model.employmentId,
        lastName: this.model.lastName,
        firstName: this.model.firstName,
        ssn: this.model.ssn,
        payRatesIdPayRates: this.model.payRatesIdPayRates,
        payRatesName: this.model.payRatesName,
      };
      this.updateEmploymentSubscription = this.employeeService.updateEmploymentPayRoll(parseInt(this.id), update).subscribe({
        next: (response) => {
          console.log(response);
          alert('Update successful');
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigateByUrl('/edit-payroll');
          });
        },
        error: err => {
          console.error('Failed to update employee:', err);
        }
      });
    }
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateEmploymentSubscription?.unsubscribe();
  }
}
