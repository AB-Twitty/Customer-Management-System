import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { CustomerInfo } from 'src/models/CustomerInfo';
import { CustomerService } from 'src/services/customer.service';
import { NationalityService } from 'src/services/nationality.service';

@Component({
  selector: 'app-add-customer',
  templateUrl: './add-customer.component.html',
  styleUrls: ['./add-customer.component.css']
})
export class AddCustomerComponent implements OnInit {
  customer!:CustomerInfo;
  nationalities:any;
  addCustomerForm!:FormGroup;

  constructor(private customerService:CustomerService, private nationalityService:NationalityService, private router:Router) { }

  ngOnInit(): void {
    this.nationalityService.getAllNationalities(true).subscribe(response => this.nationalities=response);
    var datePipe = new DatePipe("en-US");
    let formatedyear = datePipe.transform(new Date(), 'yyyy-MM-dd');
    this.addCustomerForm = new FormGroup({
      'id': new FormControl(0),
      'firstName': new FormControl(''),
      'lastName': new FormControl(''),
      'nationalId': new FormControl(''),
      'nationalityId': new FormControl(null),
      'birthDate': new FormControl(formatedyear),
      'isActive': new FormControl(false),
      'file': new FormControl(),
      'imageFile': new FormControl(),  
    })
  }

  onImageClick(imageFile:HTMLInputElement) {
    imageFile.click();
  }

  onFileChange(event:any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.addCustomerForm.patchValue({
        imageFile: file
      });
      document.querySelector('img')!.src = URL.createObjectURL(file);
    }
  }

  onAddCustomer() {
    let values = this.addCustomerForm.value;
    const uploadData = new FormData();
    for (let input_name in values) {
      if (values[input_name] instanceof Blob) // check is file
      {
        uploadData.append(input_name, values[input_name], values[input_name].name ? values[input_name].name : "");
      }
      else
        uploadData.append(input_name, values[input_name]);
    }
    this.customerService.addCustomer(uploadData).subscribe((response:any) => {
      if(response.status==200) {
        this.router.navigate([`../customer-account/${response.data.id}/details`])
      }
      else if(response.status==422) {
        response.errorMessages.forEach((element:any) => {
          if(element.prop=='nationalityId')
            this.addCustomerForm.get(element.prop)?.setErrors(["The Nationality field is required."])
          else
            this.addCustomerForm.get(element.prop)?.setErrors(element.errors);
        });
      } 
    })
  }

}
