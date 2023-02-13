import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { CustomerDetail } from 'src/models/CustomerDetail';
import { CustomerInfo } from 'src/models/CustomerInfo';
import { NationalityService } from './nationality.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  customers:CustomerInfo[] = [];
  pageCount!:number;
  customerDetails:CustomerDetail = new CustomerDetail(null,'');
  baseUrl:string = 'https://localhost:44321/api/Customer/';

  constructor(private nationalityService:NationalityService, private http:HttpClient) { }

  getAllCustomers() {
    return this.http.get(this.baseUrl+'GetAllCustomers/1').pipe(
      map((response:any) => {
        this.customers = [];
        if(response.status==200) {
          this.pageCount = response.totalCount;
          response.dataList.forEach( (element:CustomerInfo) => {
            this.nationalityService.getNationalityById(element.nationalityId).subscribe( nationality => {
              this.customers.push(new CustomerInfo(element,nationality.nationalityName));
            })
          });
        }
        return {'customers':this.customers, 'pageCount':this.pageCount} ;
      })
    )
  }

  getCustomerById(id:number) {
    return this.http.get(this.baseUrl+'CustomerDetails/'+id).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.customerDetails = new CustomerDetail(response.data,response.data.nationality.nationalityName);
          return this.customerDetails;
        }
        return null;
      })
    )
  }

  editCustomer(updatedCustomer:any) {
    return this.http.put(this.baseUrl+'UpdateCustomer', updatedCustomer);
  }

  addCustomer(uploadData:FormData) {
    return this.http.post(`${this.baseUrl}AddCustomer`, uploadData);
  }


  searchCustomers(search:any) {
    return this.http.post(this.baseUrl+'SearchCustomers', search).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.customers = [];
          this.pageCount = response.totalCount;
          response.dataList.forEach((element:any) => {
            this.nationalityService.getNationalityById(element.nationalityId).subscribe( nationality => {
              this.customers.push(new CustomerInfo(element,nationality.nationalityName));
            })
          });
        }
        return {'customers':this.customers, 'pageCount':this.pageCount};
      })
    )
  }

  addCustomerImage(imageFile:File, customerId:number) {
    let testData:FormData = new FormData();
    testData.append('imageFile', imageFile, imageFile.name);
    console.log(testData);
    return this.http.post(this.baseUrl+'AddCustomerImage/'+customerId, testData).pipe(
      map((response:any) => {
        if(response.status==200) {
            this.customerDetails = new CustomerDetail(response.data,response.data.nationalityName)
        }
        return this.customerDetails;
      })
    )
  }

  deleteCustomer(customerId:number) {
    return this.http.get(this.baseUrl+'DeleteCustomer/'+customerId).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.customerDetails.isDeleted = response.data.isDeleted;
          return this.customerDetails;
        }
        return;
      })
    )
  }

  restoreCustomer(customerId:number) {
    return this.http.get(this.baseUrl+'RestoreCustomer/'+customerId).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.customerDetails.isDeleted = response.data.isDeleted;
          return this.customerDetails;
        }
        return;
      })
    )
  }
}
