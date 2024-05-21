import { Component } from '@angular/core';
import { Observable, catchError, map, of } from 'rxjs';
import { PersonalResponse } from '../models/personal-response.model';
import { ManagerService } from '../services/manager.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manageemployee',
  templateUrl: './manageemployee.component.html',
  styleUrls: ['./manageemployee.component.css']
})
export class ManageemployeeComponent {
  id: number;
  showDeleteForm = false;
  personals$?: Observable<PersonalResponse[]>;
  constructor(private managerService: ManagerService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.personals$ = this.managerService.getAllPersonal();
    this.checkFailedDelete();
  }

  checkFailedDelete(): void {
    const failedDeleteId = localStorage.getItem('failedDeleteId');
    if (failedDeleteId) {
      this.managerService.getPersonalById(+failedDeleteId)
        .pipe(
          map(personal => [personal]), // Bao bọc đối tượng thành mảng
          catchError(error => {
            console.error('Error fetching personal by ID:', error);
            return of([]); // Trả về mảng rỗng nếu có lỗi
          })
        )
        .subscribe(personalArray => {
          this.personals$ = of(personalArray); // Gán giá trị cho biến Observable
          localStorage.removeItem('failedDeleteId'); // Xóa ID từ localStorage
        });
    }
  }
  // Phương thức gọi khi người dùng click nút xóa
  deleteAccount(personalId: number): void {
    console.log('deleteAccount called' + personalId);
    const confirmation = confirm("Bạn có muốn xoá không?");
    console.log(confirmation);
    if (confirmation) {
      this.showDeleteForm = true; // Hiển thị form xác nhận xóa
      this.id = personalId; // Lưu ID của tài khoản cần xóa
    }
  }

  // Phương thức xử lý xác nhận xóa
  confirmDelete(): void {
    // Thêm logic xóa tài khoản tại đây
    this.managerService.deleteEmployee(this.id).subscribe({
      next: (response) => {
        if (response.status === 200) {
          alert('Đã xóa thành công!');
          this.showDeleteForm = false; // Ẩn form và thực hiện xóa
          window.location.href = '/manage/personal';
        }
      },
      error: (error) => {
        alert(`Lỗi khi xóa: ${error.error.message}`);
        this.showDeleteForm = false; // Ẩn form xác nhận xóa
        this.router.navigateByUrl("/manage/employee")
      }
    });
  }

  // Phương thức hủy xóa
  cancelDelete(): void {
    this.showDeleteForm = false; // Ẩn form xác nhận xóa
  }

}
