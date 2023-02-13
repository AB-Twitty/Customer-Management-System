import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public accountService:AccountService, private router:Router) { }

  ngOnInit(): void {
  }

  onLogout() {
    this.accountService.logout().subscribe(response => this.router.navigate(['']));
  }

}
