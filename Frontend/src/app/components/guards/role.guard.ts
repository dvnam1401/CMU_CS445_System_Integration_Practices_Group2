import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from 'src/app/auth/service/auth.service';

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // Lấy quyền của người dùng từ authService
  const roles = authService.getUserRoles();
  const path = state.url;

  console.log("Current path:", path);
  console.log("User roles:", roles);

  // Xử lý truy cập vào /edit-hr
  if (path.includes('/edit-hr')) {
    if (!roles?.includes('HR')) {
      router.navigate(['/']);  // Chuyển hướng nếu không phải HR
      return false;
    }
  }

  // Xử lý truy cập vào /edit-payroll
  if (path.includes('/edit-payroll')) {
    if (!roles?.includes('PayRoll')) {
      router.navigate(['/']);  // Chuyển hướng nếu không phải PayRoll
      return false;
    }
  }

  // Kiểm tra quyền HR để truy cập /add
  if (path.includes('/add') && !roles?.includes('HR')) {
    router.navigate(['/']);  // Chuyển hướng nếu không phải HR
    return false;
  }

  // Cho phép tiếp tục nếu là HR
  if (roles?.includes('HR')) {
    return true;
  }

  // Cho phép tiếp tục nếu là PayRoll
  if (roles?.includes('PayRoll')) {
    return true;
  }

  // Chuyển hướng người dùng không có quyền thích hợp đến trang đăng nhập
  router.navigate(['/login']);
  return false;
};