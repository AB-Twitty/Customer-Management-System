import { Component, Input } from '@angular/core';
import { CustomerInfo } from 'src/models/CustomerInfo';

@Component({
  selector: 'app-customer-item',
  templateUrl: './customer-item.component.html',
  styleUrls: ['./customer-item.component.css']
})
export class CustomerItemComponent {

  @Input() customer!:CustomerInfo;
  
  constructor() { }

}
