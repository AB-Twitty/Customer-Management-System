import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomerInfo } from 'src/models/CustomerInfo';
import { CustomerService } from 'src/services/customer.service';
import { NationalityService } from 'src/services/nationality.service';

@Component({
  selector: 'app-customers-list',
  templateUrl: './customers-list.component.html',
  styleUrls: ['./customers-list.component.css']
})
export class CustomersListComponent implements OnInit {

  nationalities:any;
  pageCount = 1;
  searchForm!:FormGroup;
  customers:CustomerInfo[] = [];
  constructor(public customerService:CustomerService, private nationalityService:NationalityService, private route:ActivatedRoute) {
    this.nationalityService.getAllNationalities(true).subscribe(response => this.nationalities=response);
   }

  ngOnInit(): void {
    this.searchForm = new FormGroup({
      'name': new FormControl(),
      'nationalityId': new FormControl(),
      'startAge': new FormControl(),
      'endAge': new FormControl(),
      'isActive': new FormControl(),
      'isDeleted': new FormControl(),
      'currentPageIndex': new FormControl(1),
    })
    this.onSearch();
  }

  onSearch() {
    this.customerService.searchCustomers(this.searchForm.value).subscribe(response => {
      this.pageCount = response.pageCount;
      this.customers = response.customers;
    });
  }

  onPageClick(pageIndex:number) {
    this.searchForm.get('currentPageIndex')?.setValue(pageIndex);
    this.onSearch();
  }

  onReset() {
    this.searchForm.reset();
    this.searchForm.get('currentPageIndex')?.setValue(1);
    this.onSearch();
  }
}
