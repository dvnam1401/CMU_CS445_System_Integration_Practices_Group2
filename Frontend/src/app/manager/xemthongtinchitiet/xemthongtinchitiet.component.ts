import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { EmployeeResponse } from '../models/employee-request.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ManagerService } from '../services/manager.service';

@Component({
  selector: 'app-xemthongtinchitiet',
  templateUrl: './xemthongtinchitiet.component.html',
  styleUrls: ['./xemthongtinchitiet.component.css']
})
export class XemthongtinchitietComponent implements OnInit {
  id: number;
  employees$: Observable<EmployeeResponse[]>;
  private routeSubscription?: Subscription;
  constructor(private managerService: ManagerService,
    private route: ActivatedRoute,
    private router: Router,) { }
  ngOnInit(): void {
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = +params.get('id')!;
        this.employees$ = this.managerService.getAllEmployeeById(this.id);   
        localStorage.setItem('idPersonal', this.id.toString());     
      }
    })
  }
}
