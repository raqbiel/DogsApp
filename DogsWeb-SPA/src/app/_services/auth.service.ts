import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

baseUrl = environment.apiUrl + 'auth/';
jwtHelper = new JwtHelperService();
decodedToken: any;
token: any;
emailuser: any;
passReset = false;

constructor(private http: HttpClient) { }


login(model: any){
  return this.http.post(this.baseUrl + 'login', model)
    .pipe(
      map((response: any) => {
        const user = response;

        if (user){
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          console.log(this.decodedToken);
        }

      })
    );
}

register(model: any){
  return this.http.post(this.baseUrl + 'register', model);
}
resetpassword(model: any, token: any){
  return this.http.post(this.baseUrl + 'resetpassword?Email=' + this.emailuser + "&Token=" + this.token, model);

}

forgotpassword(model: any){
  return this.http.post(this.baseUrl + 'forgotpassword', model)
    .pipe(
      map((response: any) => {
        const user = response;
        console.log(response);
        // tslint:disable-next-line: forin
        this.emailuser = response["email"];
        for (const key in response){
           this.token = response[key];
           const element = response[key];
           console.log(element);
           console.log(this.emailuser);
       }// console.log(model);
       this.passReset = true;

      })
    );
}

resetedPassword(){
  this.passReset = true;
}

loggedIn(){
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}



}

