import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

 const cookieService = inject(CookieService);

 // check if the request should be intercepted
 if (shouldIntercepRequest(req)){

    // clone the request and set the authorization header
    const authReq = req.clone({
      setHeaders:{
        'Authorization' : cookieService.get('Authorization')
      }
    });
    return next(authReq);
  };
  
  return next(req);
};


// function to determin if the request should be intercepted
function shouldIntercepRequest (req:any):boolean {
  return req.urlWithParams.indexOf('addAuth=true') > -1;
}