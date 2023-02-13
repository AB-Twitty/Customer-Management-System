import { Component, OnInit } from '@angular/core';
import { User } from 'src/models/User';
import { AccountService } from 'src/services/account.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  currentUser!:User;
  constructor(private accountservice:AccountService) { }

  ngOnInit(): void {
    this.currentUser = new User(this.accountservice.getCurrentUser());
  }

}
