import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/services/customer.service';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css']
})
export class CustomerDetailsComponent implements OnInit {

  constructor(public customerService:CustomerService) { }

  ngOnInit(): void {
  }


  onDelete(customerId:number) {
    this.customerService.deleteCustomer(customerId).subscribe();
  }

  onRestore(customerId:number) {
    this.customerService.restoreCustomer(customerId).subscribe();
  }
}
