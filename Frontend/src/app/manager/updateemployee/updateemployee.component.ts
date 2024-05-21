import { Component } from '@angular/core';
import { AddEmployee } from '../models/add-employee.model';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { EmployeeService } from 'src/app/components/contents/services/employee.service';
import { PayRate } from '../models/pay-rate.model';
import { PersonalResponse } from '../models/personal-response.model';
import { ManagerService } from '../services/manager.service';
import { EmployeeResponse } from '../models/employee-request.model';

@Component({
  selector: 'app-updateemployee',
  templateUrl: './updateemployee.component.html',
  styleUrls: ['./updateemployee.component.css']
})
export class UpdateemployeeComponent {
  id: number;
  model: EmployeeResponse;
  payRates$: Observable<PayRate[]>
  departments$: Observable<string[]>;
  selectedPayRate?: PayRate;
  private routeSubscription?: Subscription;
  constructor(private managerService: ManagerService,
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute) {
    this.model = {
      idEmployee: 0,
      employmentCode: 0,
      lastName: '',
      firstName: '',
      ssn: 0,
      employmentStatus: '',
      hireDateForWorking: new Date(),
      numberDaysRequirementOfWorkingPerMonth: 0,
      department: '',
      payRatesIdPayRates: 0,
      payRate: '',
    }
  }

  ngOnInit(): void {
    this.payRates$ = this.managerService.getAllPayRate();
    this.payRates$.subscribe(payRates => {
      if (this.model && this.model.payRatesIdPayRates) {
        this.selectedPayRate = payRates.find(payRate => payRate.idPayRates === this.model.payRatesIdPayRates);
        this.selectedPayRate = payRates[0];
      }
    });
    console.log(this.payRates$.forEach(x => console.log(x)));
    this.departments$ = this.employeeService.getAllDepartment();
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = +params.get('id')!;
        if (this.id) {
          console.log(this.id);
          this.managerService.getEmployeeById(this.id).subscribe(
            (data) => {
              console.log(data);
              this.model = data;
              console.log(this.selectedPayRate);             
            })
        }
      }
    })

  }

  onSubmitForm(): void {
    const idPersonal = localStorage.getItem('idPersonal');
    console.log(idPersonal);
    if (this.model && this.id) {
     // const sanitizedSsn = this.model.ssn.replace(/-/g, '');
      var editEmployee: EmployeeResponse = {
        idEmployee: this.model.idEmployee,
        employmentCode: this.model.employmentCode,
        lastName: this.model.lastName,
        firstName: this.model.firstName,
        ssn: this.model.ssn,
        employmentStatus: this.model.employmentStatus,
        hireDateForWorking: this.model.hireDateForWorking,
        numberDaysRequirementOfWorkingPerMonth: this.model.numberDaysRequirementOfWorkingPerMonth,
        department: this.model.department,
        payRatesIdPayRates: this.selectedPayRate?.idPayRates ?? 0,
        payRate: this.selectedPayRate?.payRateName?? '',
      };
      console.log(editEmployee);
      this.managerService.updateEmployee(this.id, editEmployee).subscribe(
        (data) => {
          console.log(data);
          this.router.navigate(['/manage/thongtinchitiet-employee', idPersonal]);
          localStorage.removeItem('idPersonal'); // Xóa ID từ localStorage
        }
      )
    }
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
  }
}
