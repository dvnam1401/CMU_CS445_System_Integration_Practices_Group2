import { Component, OnDestroy, OnInit } from '@angular/core';
import { Employment } from '../models/update-employment.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})

export class EditComponent implements OnInit, OnDestroy {
  id: string | null = null;
  model?: Employment;
  routeSubscription?: Subscription;
  updateEmploymentSubscription?: Subscription;

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private router: Router,

  ) {
  }

  ngOnInit(): void {
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
      this.employeeService.getEmploymentById(parseInt(this.id)).subscribe({
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
      var update: Employment = {
        employmentId: this.model.employmentId,
        lastName: this.model.lastName,
        firstName: this.model.firstName,
        shareholderStatus: this.model.shareholderStatus,
        phoneNumber: this.model.phoneNumber,
        email: this.model.email,
        address: this.model.address,
      };
      this.updateEmploymentSubscription = this.employeeService.updateEmploymentHr(parseInt(this.id), update).subscribe({
        next: (response) => {
          console.log(response);
          alert('Update successful');
          this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
            this.router.navigateByUrl('/edit-hr');
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
