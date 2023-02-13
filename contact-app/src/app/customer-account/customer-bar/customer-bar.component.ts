import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/services/customer.service';

@Component({
  selector: 'app-customer-bar',
  templateUrl: './customer-bar.component.html',
  styleUrls: ['./customer-bar.component.css']
})
export class CustomerBarComponent implements OnInit {

  imageFile!:File;
  constructor(public customerService:CustomerService) {}

  ngOnInit(): void {
  }

  onImageClick(imageInput:HTMLInputElement) {
    imageInput.click();
  }

  onImageChange(event:any) {
    if(event) {
      this.imageFile = event?.target?.files[0];
      this.customerService.addCustomerImage(this.imageFile, this.customerService.customerDetails.id).subscribe();
    }
  }
}
