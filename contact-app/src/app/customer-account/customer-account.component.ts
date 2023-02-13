import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CustomerService } from 'src/services/customer.service';

@Component({
  selector: 'app-customer-account',
  templateUrl: './customer-account.component.html',
  styleUrls: ['./customer-account.component.css']
})
export class CustomerAccountComponent implements OnInit {

  constructor(private customerService:CustomerService, private route:ActivatedRoute) {
    this.customerService.getCustomerById(+this.route.snapshot.params['customerId']).subscribe();
   }

  ngOnInit(): void {
  } 

}
