import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/models/User';
import { AccountService } from 'src/services/account.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  user:User = new User(this.accountService.getCurrentUser());
  editAccountForm!:FormGroup;

  constructor(private accountService:AccountService, private router:Router) {

   }

  ngOnInit() {
    this.editAccountForm = new FormGroup({
      'userId': new FormControl(this.user.userId),
      'name': new FormControl(this.user.name),
      'username': new FormControl(this.user.username),
      'email': new FormControl(this.user.email)
    })
  }

  onEditProfile() {
    this.accountService.editProfile(this.editAccountForm.value).subscribe( response => {
      if(response.errorMessages!=null && response.status==422) {
        response.errorMessages.forEach((element:any) => {
          this.editAccountForm.get(element.prop)?.setErrors(element.errors);
        });
      } else if(response.status==200) {
        this.router.navigate(['/account/profile']);
      }
    });
  }
}
