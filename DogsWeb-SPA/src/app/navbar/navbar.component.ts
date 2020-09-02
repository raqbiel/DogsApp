import { AlertsService } from './../_services/alerts.service';
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


  constructor(public authService: AuthService, private alerts: AlertsService) { }

  ngOnInit(): void {

  }

  login(){
   this.authService.login(this.model).subscribe(next => {
      this.alerts.onSuccess('Zalogowany! Witaj ' + this.model.username);
   }, err => {
     this.alerts.onError(err);
     console.log(err);
   });
  }

  loggedIn(){
   return this.authService.loggedIn();
  }

  logout(){
    localStorage.removeItem('token');
    this.alerts.onInfo('Nastąpiło wylogowanie');
  }

}
