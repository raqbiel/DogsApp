import { User } from '../_models/user';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class UserService {

baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getUsers(): Observable<User[]> {
  return this.http.get<User[]>(this.baseUrl + 'users');

}
getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

confirmEmail(): Observable<User> {

  return this.http.get<User>(this.baseUrl + 'users/');
}

updateUser(id: string, user: User){
  return this.http.put(this.baseUrl + 'users/' + id, user);
}

}
