import { ForgotPasswordComponent } from './../forgotPassword/forgotPassword.component';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertsService } from '../_services/alerts.service';

@Component({
  selector: 'app-resetPassword',
  templateUrl: './resetPassword.component.html',
  styleUrls: ['./resetPassword.component.scss']
})
export class ResetPasswordComponent implements OnInit {


   model: any = {};
  errors: any = {};
  @Input()passwordReseted = false;
  @Input()registerMode = true;

  constructor(public authService: AuthService, private alerts: AlertsService) { }

  ngOnInit() {
  }

  reset(){
  this.model.email = this.authService.emailuser;
  if (this.model.token === this.authService.token){
    this.authService.resetpassword(this.model, this.model).subscribe(() => {
      this.alerts.onSuccess('Hasło zmienione!');
      this.passwordReseted = true;
      this.registerMode = false;
      // console.log(this.model.token);
      // console.log(this.authService.token);
    }, error => {
      this.errors = error;
      this.alerts.onError(error);
      console.log(error);
    });
  }else{
    this.alerts.onError('Nieprawidłowy token! Sprawdz skrzynkę pocztową.');
  }
  }




}
