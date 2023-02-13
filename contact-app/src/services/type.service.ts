import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { ContactType } from 'src/models/ContactType';

@Injectable({
  providedIn: 'root'
})
export class TypeService {

  contactTypes:ContactType[] = [];
  type:ContactType = new ContactType(null);
  baseUrl:string = 'https://localhost:44321/api/ContactType/';
  constructor(private http:HttpClient) { }

  getContactTypes() {
    return this.http.get(this.baseUrl+'getContactTypes').pipe(
      map((response:any) => {
        this.contactTypes = [];
        if(response.status==200) {
          response.dataList.forEach((element:ContactType) => {
            this.contactTypes.push(element);
          });
        }
        return this.contactTypes;
      })
    )
  }

  getTypeById(typeId:number) {
    return this.http.get(this.baseUrl+'GetType/'+typeId).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.type = new ContactType(response.data);
          return this.type;
        }
        return;
      })
    )
  }
}
