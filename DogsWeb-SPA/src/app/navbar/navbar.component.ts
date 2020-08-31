import { AuthService } from '../_services/auth.service';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class NavbarComponent implements OnInit {

  model: any = {};
  hide = true;
  form: FormGroup;
  err: any = {};


  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    console.log(this.err.toString());
  }

  login(){
   this.authService.login(this.model).subscribe(next => {
      console.log('Logged successfully!');
   }, err => {
     console.log('Failed login', err);
   });
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return !!token; // jeżeli cos zwróci to true, puste to false
  }

  logout(){
    localStorage.removeItem('token');
    console.log('Logout');
  }

}
