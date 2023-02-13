import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  usernameOrEmail!:string;
  password!:string;

  loginForm!:FormGroup;
  constructor(private accountService:AccountService, private router:Router, private http:HttpClient) {
  }

  ngOnInit() : void {
    this.loginForm = new FormGroup({
      'usernameOrEmail': new FormControl('',Validators.required),
      'password': new FormControl('', Validators.required),
    })
  }

  onLogin() {
    if(this.loginForm.valid)
      this.accountService.login(this.loginForm.value).subscribe(response => {
        if(response!=null)
          this.router.navigate(['/account/profile']);
      });
  }

  onSubmit() {
    /*const formData = new FormData();
    formData.append("usernameOrEmail", this.loginForm.get("usernameOrEmail")?.value);
    formData.append("password", this.loginForm.get("password")?.value);
    /*this.http.post("https://localhost:44319/api/Account/Login", formData, {
      headers : new HttpHeaders().set("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2NjU2NTQ4MTgsImV4cCI6MTY2NjI1OTYxNywiaWF0IjoxNjY1NjU0ODE4fQ.vyjHni_qqrpaS0lE20TRh03QOF9wME7xTJa4LUZ_m1c")
    }).subscribe(request => console.log(request))*/
    this.http.get("https://localhost:44319/api/Account/AccountInfo", {
      params : {accountId:1},
      headers : new HttpHeaders().set("Authorization", "hyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2NjU2NTQ4MTgsImV4cCI6MTY2NjI1OTYxNywiaWF0IjoxNjY1NjU0ODE4fQ.vyjHni_qqrpaS0lE20TRh03QOF9wME7xTJa4LUZ_m1c")
    }).subscribe(request => console.log(request))
  }
}
