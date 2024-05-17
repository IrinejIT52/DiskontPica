import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../../../services/customer.service';
import { Customer } from '../../../models/customer';
import { HttpStatusCode } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,RouterOutlet, RouterLink, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  

  constructor(private customerService:CustomerService, private router:Router) { }

  customer:Customer= {
    name: '',
    password: '',
    email: '',
    adress: '',
    customerId: 0,
    salt: ''
  };



  onRegister(){
    this.customerService.GetCustomerByName(this.customer.name).subscribe((data)=>{
      if(data[0] != null)
        {
          alert('Name alreday exists!')
        }
      else
      {
        this.customerService.AddCustomer(this.customer).subscribe((data)=>{
          if(data != HttpStatusCode.BadRequest)
          {
              this.router.navigateByUrl('/login')
          }
          
            
      })
      }
    })

   
  }

    
  

  toLogin(){
    this.router.navigateByUrl('/login')
  }

}
