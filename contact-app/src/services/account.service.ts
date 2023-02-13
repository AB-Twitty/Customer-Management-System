import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/models/User';
import { map } from 'rxjs';
import { Registration } from 'src/models/Registration';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  currentUser!:User;
  baseUrl = 'https://localhost:44321/api/User/';

  constructor(private http:HttpClient) { }

  login(loginModel:any) {
    return this.http.post(this.baseUrl+"Login", loginModel).pipe(
      map( (response:any) => {
        if(response.data) {
          this.currentUser = new User(response.data);
          sessionStorage.setItem('user', JSON.stringify(this.currentUser));
          return this.currentUser; 
        }
        return null;
      })
    )
  }

  register(registration:Registration) {
    return this.http.post(this.baseUrl+'Register', registration).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.currentUser = new User(response.data);
          sessionStorage.setItem('user', JSON.stringify(this.currentUser));
          return this.currentUser; 
        } else if (response.status==422) {
          return response;
        }
        return null;
      })
    )
  }

  editProfile(user:User) {
    return this.http.put(this.baseUrl+'UpdateUser', user).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.currentUser = new User(response.data);
          this.setCurrentUser(this.currentUser);
          return this.currentUser; 
        } else if(response.status==422) {
          return response;
        }
        return null;
      })
    )
  } 

  logout() {
    return this.http.get(this.baseUrl+'Logout').pipe(
      map( () => {
        sessionStorage.removeItem('user');
        this.currentUser = new User(null);
      })
    )
  }

  getCurrentUser() {
    const x = sessionStorage.getItem('user');
    if(x) {
      this.currentUser = new User(JSON.parse(x));
      return this.currentUser;
    }
    return null;
  }

  setCurrentUser(user:User) {
    const x = sessionStorage.getItem('user');
    if(x) {
      sessionStorage.removeItem('user');
      sessionStorage.setItem('user', JSON.stringify(user));
    }
  }
}
