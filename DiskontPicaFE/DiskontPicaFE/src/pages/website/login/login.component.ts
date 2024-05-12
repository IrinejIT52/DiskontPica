import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Principal } from '../../../models/principal';
import { LoginService } from '../../../services/login.service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,RouterOutlet, RouterLink, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  parseJwt(token: string) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
  }

  constructor(private loginService:LoginService, private router:Router) { }

  principal:Principal= {
    name:'',
    password:''
  };

  onLogin(){
        this.loginService.login(this.principal).subscribe(
          data => {
              if(data != HttpErrorResponse)
                {
                  localStorage.setItem('token',data.token)
                  const{admin} = this.parseJwt(data.token)
                  if(admin == "True")
                  {
                    this.router.navigateByUrl('/dashboard')
                  }
                  else
                  {
                    this.router.navigateByUrl('/landing-page')
                  }
                  
                }
              else
              {
                alert("Wrong Credentials")
              }
            }
          )
          
          
          
  }

   
    
}
    






