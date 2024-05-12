import { HttpInterceptorFn } from "@angular/common/http";
import { catchError, throwError } from "rxjs";

export const httpErrorIntercepter: HttpInterceptorFn = (req,next) => {
    return next(req).pipe(catchError((error)=> {

            if([401,403].includes(error.status)){
                alert("Unautherized");
            }
            else if([404].includes(error.status)){
                alert("Not Found");
            }
            

            return throwError(()=>error)
        }
    ))
}