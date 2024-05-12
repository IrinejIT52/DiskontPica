import { HttpErrorResponse, HttpInterceptorFn } from "@angular/common/http";
import { catchError } from "rxjs";

export const authIntercepter: HttpInterceptorFn = (req, next) => {
    const jwtToken = getJwtToken();

    if(jwtToken){
        req.clone({
            setHeaders:{
                Authorization:`Bearer ${jwtToken}`
            }
        })  
    }
    return next(req)
    
};

function getJwtToken(): string | null {
    return localStorage.getItem('token');
}