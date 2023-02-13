import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Contact } from 'src/models/Contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  contacts:Contact[] = [];
  contact:Contact = new Contact(null);

  baseUrl:string = 'https://localhost:44321/api/Contact/'
  constructor(private http:HttpClient) { }

  getContactsFor(customerId:number) {
    return this.http.get(this.baseUrl+'GetContactsFor/'+customerId).pipe(
      map((response:any) => {
        this.contacts = [];
        if(response?.dataList) {
          response.dataList.forEach((element:any) => {
            this.contacts.push(new Contact(element));
          });
        }
        return this.contacts;
      })
    )
  }

  getContactById(id:number) {
    return this.http.get(this.baseUrl+'GetContact/'+id).pipe(
      map((response:any) => {
        this.contact = new Contact(null);
        if(response.status==200)
          this.contact = new Contact(response.data);
        return this.contact;
      })
    )
  }

  addContact(contact:any) {
    return this.http.post(this.baseUrl+'AddContactTo/'+contact.customerId, contact);
  }

  deleteContact(contactId:number) {
    return this.http.get(this.baseUrl+'DeleteContact/'+contactId).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.contacts.forEach(element => {
            if(element.id===response.data.id) {
              element.isDeleted = response.data.isDeleted;
              element.isActive = response.data.isActive;
            }
          });
        }
      })
    )
  }

  restoreContact(contactId:number) {
    return this.http.get(this.baseUrl+'RestoreContact/'+contactId).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.contacts.forEach(element => {
            if(element.id===response.data.id) {
              element.isDeleted = response.data.isDeleted;
              element.isActive = response.data.isActive;
            }
          });
        }
      })
    )
  }

  editContact(contact:any) {
    return this.http.put(this.baseUrl+'UpdateContact', contact);
  }
}
