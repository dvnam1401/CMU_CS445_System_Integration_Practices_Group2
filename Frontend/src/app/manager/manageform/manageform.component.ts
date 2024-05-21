import { Component, OnInit } from '@angular/core';
import { PersonalResponse } from '../models/personal-response.model';
import { Observable } from 'rxjs';
import { ManagerService } from '../services/manager.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manageform',
  templateUrl: './manageform.component.html',
  styleUrls: ['./manageform.component.css']
})
export class ManageformComponent implements OnInit {
  id: number;
  showDeleteForm = false;
  personals$?: Observable<PersonalResponse[]>;
  constructor(private managerService: ManagerService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.personals$ = this.managerService.getAllPersonal();
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
    this.managerService.deletePersonal(this.id).subscribe({
      next: (response) => {
        if (response.status === 200) {
         // alert('Đã xóa thành công!');
          this.showDeleteForm = false; // Ẩn form và thực hiện xóa
          window.location.href = '/manage/personal';
        }
      },
      error: (error) => {
        alert(`Lỗi khi xóa: Hãy xóa lịch sử làm việc trước !!!`);
        this.showDeleteForm = false; // Ẩn form xác nhận xóa
        localStorage.setItem('failedDeleteId', this.id.toString());
        this.router.navigateByUrl("/manage/employee")
      }
    });
  }

  // Phương thức hủy xóa
  cancelDelete(): void {
    this.showDeleteForm = false; // Ẩn form xác nhận xóa
  }
}
