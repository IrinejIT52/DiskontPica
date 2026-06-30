import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

function parseJwt(token: string): any {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split('')
        .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
    return JSON.parse(jsonPayload);
  } catch {
    return null;
  }
}

export const adminGuard: CanActivateFn = () => {
  const router = inject(Router);
  const token = localStorage.getItem('token');

  if (!token) {
    router.navigateByUrl('/login');
    return false;
  }

  const payload = parseJwt(token);

  if (payload && payload['admin'] === 'True') {
    return true;
  }

  // Regular user or invalid token – redirect to products page
  router.navigateByUrl('/product');
  return false;
};
