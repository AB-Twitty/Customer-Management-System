import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/services/account.service';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {

  constructor(public accountService:AccountService, private router:Router) { }

  ngOnInit(): void {
  }

  onLogout() {
    this.accountService.logout().subscribe(response => this.router.navigate(['']));

  }
}
