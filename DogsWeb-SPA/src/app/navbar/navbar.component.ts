import { AlertsService } from './../_services/alerts.service';
import { AuthService } from '../_services/auth.service';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

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
  public isCollapsed = false;

  constructor(public authService: AuthService, private alerts: AlertsService, private router: Router) { }

  ngOnInit(): void {

  }

  login(){
   this.authService.login(this.model).subscribe(next => {
      this.alerts.onSuccess('Użytkownik zalogowany!');
   }, err => {
     this.alerts.onError(err);
   }, () => {
     this.router.navigate(['/lista']);
   });
  }

  loggedIn(){
   return this.authService.loggedIn();
  }

  logout(){
    localStorage.removeItem('token');
    this.alerts.onInfo('Nastąpiło wylogowanie');
    this.router.navigate(['/home']);
  }

}
