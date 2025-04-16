import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const cookieService = inject(CookieService);

  // Check if the request should be intercepted
  if (shouldInterceptRequest(req)) {

    // Clone the request and set the Authorization header
    const authReq = req.clone({
      setHeaders: {
        'Authorization': cookieService.get('Authorization')
      }
    });

    return next(authReq);
  }

  return next(req);
 
};

// Function to determine if the request should be intercepted
function shouldInterceptRequest(req: any): boolean {
  return req.urlWithParams.indexOf('addAuth=true') > -1;
}


