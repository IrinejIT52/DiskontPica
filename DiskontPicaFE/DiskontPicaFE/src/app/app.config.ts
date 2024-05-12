import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authIntercepter } from '../services/login.intercepter';
import { httpErrorIntercepter } from '../services/httperror.intercepter';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes),provideHttpClient(withInterceptors([authIntercepter,httpErrorIntercepter]))]
 
};
