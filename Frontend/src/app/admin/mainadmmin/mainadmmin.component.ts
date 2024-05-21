import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserRequest } from '../models/user-request.model';
import { Observable, Subscription } from 'rxjs';
import { AdminService } from '../services/admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPassword } from '../models/reset-password.model';

@Component({
  selector: 'app-mainadmmin',
  templateUrl: './mainadmmin.component.html',
  styleUrls: ['./mainadmmin.component.css']
})
export class MainadmminComponent implements OnInit, OnDestroy {
  id: number;
  showResetForm: boolean = false;
  showDeleteForm = false; // Biến để kiểm soát việc hiển thị form xác nhận xóa
  newPassword: string;
  confirmPassword: string;
  users$?: Observable<UserRequest[]>;
  submitted: boolean = false;
  searchText: string | null = null;
  updatePasswordSubscription?: Subscription;
  constructor(private adminService: AdminService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.users$ = this.adminService.getAllUser();
  }

  // toggleResetForm(userId: number) {
  //   console.log('toggleResetForm called');
  //   this.id = userId;
  //   console.log('User ID: ' + this.id);
  //   this.showResetForm = !this.showResetForm;
  // }
  toggleResetForm(userId: number): void {
    this.id = userId;
    this.showResetForm = true;
    this.submitted = false;
  }
  updatePassword(): void {
    this.submitted = true;
    if (!this.newPassword || !this.confirmPassword) {
      alert("Please fill out all password fields.");
      return;
    }

    if (this.newPassword !== this.confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    const resetPassword: ResetPassword = {
      userId: this.id,
      newPassword: this.newPassword,
    };

    this.adminService.resetPasword(resetPassword).subscribe({
      next: () => {
        alert("Password has been successfully updated!");
        this.showResetForm = false;
        this.submitted = false;
        this.newPassword = '';
        this.confirmPassword = '';
      },
      error: (error) => {
        alert(`Error updating password: ${error.message}`);
      }
    });
  }

  findEmployee(): void {
    if (this.searchText) {
      console.log('searchText:'+ this.searchText);
      this.users$ = this.adminService.findAccountUser(this.searchText);
    }
    else {
      this.users$ = this.adminService.getAllUser();
    }
  }
  
  cancelResetForm(): void {
    this.showResetForm = false;
  }

  // Phương thức gọi khi người dùng click nút xóa
  deleteAccount(userId: number): void {
    this.id = userId;
    const confirmation = confirm("Bạn có muốn xoá không?");
    if (confirmation) {
      this.showDeleteForm = true; // Hiển thị form xác nhận xóa
    }
  }

  // Phương thức xử lý xác nhận xóa
  confirmDelete(): void {   
    this.adminService.deleteUser(this.id).subscribe({
      next: (response) => {
        if (response.status === 200) {
          alert('Đã xóa thành công!');
          this.showDeleteForm = false; // Ẩn form và thực hiện xóa
          window.location.href = '/admin';
        }
      },
      error: (error) => {
        alert(`Lỗi khi xóa: ${error.error.message}`);
        this.showDeleteForm = false; // Ẩn form xác nhận xóa
        //this.router.navigateByUrl("/manage/employee")
      }
    });



  }

  // Phương thức hủy xóa
  cancelDelete(): void {
    this.showDeleteForm = false; // Ẩn form xác nhận xóa
  }
  ngOnDestroy(): void {
    this.updatePasswordSubscription?.unsubscribe();
  }
}
