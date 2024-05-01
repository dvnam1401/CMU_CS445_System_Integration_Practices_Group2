import { Component, OnInit } from '@angular/core';
import { EmployeeNotification } from '../models/employment-anniversary.model';
import { Observable, catchError, switchMap } from 'rxjs';
import { EmployeeService } from '../services/employee.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-notification-hiring',
  templateUrl: './notification-hiring.component.html',
  styleUrls: ['./notification-hiring.component.css']
})
export class NotificationHiringComponent implements OnInit {
  employment$?: Observable<EmployeeNotification[]>;
  titles?: string;
  daysLimit: number = 12;
  constructor(private service: EmployeeService
    ) { }

    ngOnInit(): void {
      this.onDaysLimitChange(); // Gọi ngay khi component khởi tạo nếu bạn muốn
    }
  
    onDaysLimitChange(): void {
      console.log('Days Limit changed to:', this.daysLimit);
      this.employment$ = this.service.getEmploymentAnniversary(this.daysLimit);
    }
}
