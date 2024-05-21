import { CookieService } from 'ngx-cookie-service';
import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { jwtDecode } from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {
  // const cookieService = inject(CookieService);
  // const authService = inject(AuthService);
  // const router = inject(Router);
  // const user = authService.getUser();
  // // check for the jwet token
  // let token = cookieService.get('Authorization');

  // if (token && user) {
  //   token = token.replace('Bearer ', '');
  //   const decodedToken: any = jwtDecode(token);
  //   //check if token has expired
  //   const expirationDate = decodedToken.exp * 10000000;
  //   const currentTime = new Date().getTime();

  //   if (expirationDate < currentTime) {
  //     authService.logout();
  //     return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
  //   } else {
  //     //token is still valid
  //     if (user) {
  //       return true;
  //     } else {
  //       //user is not logged in
  //       return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
  //     }
  //   }
  // } else {
  //   //logout
  //   authService.logout();
  //   return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
  // }

  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const user = authService.getUser();

  let token = cookieService.get('Authorization');
  if (token && user) {
    token = token.replace('Bearer ', '');
    const decodedToken: any = jwtDecode(token);
    // Kiểm tra quyền truy cập dựa trên role
    //var roles = decodedToken.roles; // Giả sử role được lưu trữ trong 'roles' trong token
    const roles = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    console.log(roles);
    // Kiểm tra quyền read
    if (roles.includes('Read')) {
      console.log('true');
      return true;
    }

    // Kiểm tra quyền admin
    if (roles.includes('Admin')) {
      // Kiểm tra path admin
      if (state.url.startsWith('/admin')) {
        return true;
      } else {
        return router.createUrlTree(['/admin'], { queryParams: { returnUrl: state.url } });
      }
    }

    // Kiểm tra quyền write, delete, edit
    if (roles.includes('Write') || roles.includes('Delete') || roles.includes('Edit')) {
      // Kiểm tra path manage
      if (state.url.startsWith('/manage')) {
        return true;
      } else {
        return router.createUrlTree(['/manage'], { queryParams: { returnUrl: state.url } });
      }
    }

    // Nếu không có quyền phù hợp
    return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
  } else {
    // logout
    authService.logout();
    return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
  }
};
