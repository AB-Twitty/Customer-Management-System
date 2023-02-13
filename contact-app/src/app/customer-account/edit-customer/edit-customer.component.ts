import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerDetail } from 'src/models/CustomerDetail';
import { CustomerService } from 'src/services/customer.service';
import { NationalityService } from 'src/services/nationality.service';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent implements OnInit {

  nationalities:any;
  customer:any;
  editCustomerForm!:FormGroup;
  constructor(private customerService:CustomerService, private nationalityService:NationalityService, 
      private route:ActivatedRoute, private router:Router) {
    this.editCustomerForm = new FormGroup({
      'id': new FormControl(),
      'firstName': new FormControl(),
      'lastName': new FormControl(),
      'nationalId': new FormControl(),
      'birthDate': new FormControl(),
      'nationalityId': new FormControl(),
      'isActive': new FormControl(),
    })
  }

  ngOnInit(): void {
    this.nationalityService.getAllNationalities(true).subscribe(response => this.nationalities=response);
    this.customerService.getCustomerById(+this.route.parent?.snapshot.params['customerId']).subscribe(response => {
      if(response!=null) {
        var datePipe = new DatePipe("en-US");
        let formatedyear = datePipe.transform(response.birthDate, 'yyyy-MM-dd');
        this.editCustomerForm.get('id')?.setValue(response.id);
        this.editCustomerForm.get('firstName')?.setValue(response.firstName);
        this.editCustomerForm.get('nationalId')?.setValue(response.nationalId);
        this.editCustomerForm.get('lastName')?.setValue(response.lastName);
        this.editCustomerForm.get('nationalityId')?.setValue(response.nationalityId);
        this.editCustomerForm.get('birthDate')?.setValue(formatedyear);
        this.editCustomerForm.get('isActive')?.setValue(response.isActive);
      }
    });
  }

  onEdit() {
    this.customerService.editCustomer(this.editCustomerForm.value).subscribe((response:any) => {
      if(response.status==200) {
        this.router.navigate(["../details"], {relativeTo:this.route});
        this.customerService.customerDetails.isActive = response.data.isActive;
      }
      else if (response.status==422) {
        response.errorMessages.forEach((element:any) => {
          this.editCustomerForm.get(element.prop)?.setErrors(element.errors);
        });
      }
    });
  }
}
