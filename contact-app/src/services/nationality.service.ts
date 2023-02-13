import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NationalityService {

  baseUrl:string = 'https://localhost:44321/api/Nationality/';

  constructor(private http:HttpClient) { }

  getNationalityById(id:number) {
    return this.http.get(this.baseUrl+'GetNationality/'+id).pipe(
      map( (response:any) => {
        if(response.data)
          return response.data;
        return null;
      })
    )
  }

  getAllNationalities(isActive:boolean) {
    return this.http.get(this.baseUrl+'NationalitiesList/'+isActive).pipe(
      map((response:any) => {
        if(response.status==200)
          return response.dataList;
        return;
      })
    )
  }
}
