import { Component, OnInit } from '@angular/core';
import { Registration } from 'src/models/Registration';
import { AccountService } from 'src/services/account.service';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registration!:Registration;
  
  registrationForm!:FormGroup;

  constructor(private accountService:AccountService, private router:Router) { }

  ngOnInit(): void {
    this.registrationForm = new FormGroup({
      'name': new FormControl(''),
      'username': new FormControl(''),
      'email': new FormControl(''),
      'password': new FormControl(''),
      'confirmPassword': new FormControl(''),
    })
  }

  onRegister() {
    this.registration = new Registration(this.registrationForm.value);
    this.accountService.register(this.registration).subscribe( response => {
      if(response.errorMessages) {
        response.errorMessages.forEach((element:any) => {
          this.registrationForm.get(element.prop)?.setErrors(element.errors);
        });
      } else {
        this.router.navigate(['/account/profile']);
      }
    });
  }

}
