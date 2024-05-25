import { Component, OnDestroy, OnInit } from '@angular/core';
import { EmployeeTotal } from '../models/employee-total-request.model';
import { Observable, Subscription, catchError, switchMap } from 'rxjs';
import { EmployeeService } from '../services/employee.service';
import { EmployeeFilter } from '../models/employee-filter.model';
import { ActivatedRoute } from '@angular/router';
import { EthnicitySelectorComponent } from '../ethnicity-selector/ethnicity-selector.component';
import { Ethnicity } from '../models/ethnicity-enum.model';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit, OnDestroy {
  private filter?: Subscription;
  model: EmployeeFilter = {
    isAscending: null,
    gender: null,
    category: null,
    ethnicity: null,
    department: null,
    year: null,
    month: null,
    shareholderStatus: null
  };
  employeeSalary$?: Observable<EmployeeTotal[]>;
  title?: string;
  // selectedEthnicity: EthnicitySelectorComponent;
  selectedEthnicity = Object.values(Ethnicity);
  selectedDate: string;
  departments$?: Observable<string[]>;
  constructor(private service: EmployeeService,
    private route: ActivatedRoute,) {
    // this.selectedEthnicity = new EthnicitySelectorComponent();
    this.departments$ = this.service.getAllDepartment();
  }
  ngOnDestroy(): void {
    this.filter?.unsubscribe();
  }

  ngOnInit(): void {
    // Lấy thông tin từ route và khởi tạo truy vấn ban đầu
    this.filter = this.route.data.subscribe(data => {
      this.title = data['title'];
      this.onFilterChange();  // Khởi tạo dữ liệu ban đầu dựa trên bộ lọc
    });
  }

  onFilterChange(): void {
    const date = new Date(this.selectedDate);
    this.model.year = date.getFullYear();
    this.model.month = date.getMonth() + 1;
    console.log(`Year: ${this.model.year}, Month: ${this.model.month}`);
    // Cập nhật dữ liệu dựa trên bộ lọc hiện tại
    this.employeeSalary$ = this.route.data.pipe(
      switchMap(data => {
        const type = data['type'];
        return this.fetchDataBasedOnType(type);
      }),
      catchError(error => {
        console.error('Error fetching data:', error);
        return [];  // Xử lý lỗi và cung cấp một giá trị fallback
      })
    );
  }

  fetchDataBasedOnType(type: string): Observable<EmployeeTotal[]> {
    // Dựa vào loại truy vấn, gửi yêu cầu tới service với bộ lọc hiện tại
    console.log(this.model.department);
    switch (type) {
      case 'totalEarnings':
        return this.service.getEmployeeSalary(this.model);
      case 'vacationDays':
        return this.service.getEmployeeNumberVacation(this.model);
      // case 'averageBenefits':
      //   return this.service.getEmployeeAvergeBenefit(this.model);
      default:
        return this.service.getEmployeeAvergeBenefit(this.model);
    }
  }
}
