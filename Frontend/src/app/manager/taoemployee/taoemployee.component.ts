import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AddUser } from '../../admin/models/add-user.model';
import { AdminService } from '../../admin/services/admin.service';
import { Observable, Subscription } from 'rxjs';
import { Groups } from '../../admin/models/group.model';
import { PersonalResponse } from '../models/personal-response.model';
import { PayRate } from '../models/pay-rate.model';
import { ManagerService } from '../services/manager.service';
import { EmployeeService } from 'src/app/components/contents/services/employee.service';
import { AddEmployee } from '../models/add-employee.model';

@Component({
  selector: 'app-taoemployee',
  templateUrl: './taoemployee.component.html',
  styleUrls: ['./taoemployee.component.css']
})
export class TaoemployeeComponent implements OnInit, OnDestroy {
  id: number;
  model: AddEmployee;
  personal$: PersonalResponse;
  payRates$: Observable<PayRate[]>
  departments$: Observable<string[]>;
  selectedPayRate?: PayRate;
  private routeSubscription?: Subscription;
  constructor(private managerService: ManagerService,
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute) {
    this.model = {
      employmentCode: 'string',
      lastName: '',
      firstName: '',
      ssn: 0,
      employmentStatus: '',
      hireDateForWorking: new Date(),
      numberDaysRequirementOfWorkingPerMonth: 0,
      department: '',
      payRatesIdPayRates: 0,
      payRate: '',
      personalId: 0
    }
  }

  ngOnInit(): void {
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = +params.get('id')!;
        if (this.id) {
          this.managerService.getPersonalById(this.id).subscribe(
            (data) => {
              this.personal$ = data;
            })
        }
      }
    })
    this.payRates$ = this.managerService.getAllPayRate();
    this.departments$ = this.employeeService.getAllDepartment();
  }

  onSubmitForm(): void {
    if (this.model && this.id) {
      const sanitizedSsn = this.personal$.socialSecurityNumber.replace(/-/g, '');
      var addEmployee: AddEmployee = {
        employmentCode: (this.model.employmentCode).toString(),
        lastName: this.personal$.currentLastName + " " + this.personal$.currentMiddleName,
        firstName: this.personal$.currentFirstName,
        ssn: Number(sanitizedSsn),
        employmentStatus: this.model.employmentStatus,
        hireDateForWorking: this.model.hireDateForWorking,
        numberDaysRequirementOfWorkingPerMonth: this.model.numberDaysRequirementOfWorkingPerMonth,
        department: this.model.department,
        payRatesIdPayRates: this.selectedPayRate?.idPayRates ?? 0,
        payRate: this.selectedPayRate?.payRateName ?? '',
        personalId: this.id,
      };
      console.log(addEmployee);
      this.managerService.addEmployee(addEmployee).subscribe(
        (data) => {
          console.log(data);
          this.router.navigate(['/manage/employee']);
        }
      )
    }
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
  }
}
