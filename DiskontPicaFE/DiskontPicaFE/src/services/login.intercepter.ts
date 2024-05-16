import { HttpInterceptorFn } from "@angular/common/http";


export const authIntercepter: HttpInterceptorFn = (req, next) => {
    const jwtToken = getJwtToken();
    
    const modifidyReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${jwtToken}`),
      });
   
    return next(modifidyReq)
    
};

function getJwtToken(): string | null {
    return localStorage.getItem('token');
}