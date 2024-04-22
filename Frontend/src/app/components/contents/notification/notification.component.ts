import { Component, OnInit } from '@angular/core';
import { Observable, catchError, switchMap } from 'rxjs';
import { EmployeeNotification } from '../models/employment-anniversary.model';
import { EmployeeService } from '../services/employee.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
  employment$?: Observable<EmployeeNotification[]>;
  titles?: string;
  constructor(private service: EmployeeService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.employment$ = this.route.data.pipe(
      switchMap(data => {
        const type = data['type'];
        return this.fetchDataBasedOnType(type);
      }),
      catchError(error => {
        console.error('Error fetching data:', error);
        return [];
      })
    )
  }
  fetchDataBasedOnType(type: string): Observable<EmployeeNotification[]> {
    switch (type) {
      case 'anniversary':
        console.log('anniversary');
        return this.service.getEmploymentAnniversary();
      case 'birthday':
        console.log('birthday');
        return this.service.getEmployeeBirthday();
      case 'vacation':
        console.log('vacation');

        return this.service.getVacationEmployeeThisYear();
      default:
        return this.service.getEmployeeBirthday();
    }
  }
}
