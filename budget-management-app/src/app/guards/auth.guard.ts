import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { SessionService } from '../services/session.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const sessionService = inject(SessionService);

  if (sessionService.isTokenValid) {
    return true;
  } else {
    router.navigateByUrl('/login');
    return false;
  }
};
