import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertsService } from '../_services/alerts.service';

@Component({
  selector: 'app-forgotPassword',
  templateUrl: './forgotPassword.component.html',
  styleUrls: ['./forgotPassword.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  model: any = {};
  errors: any = {};

  forgotBool = false;

  constructor(public authService: AuthService, private alerts: AlertsService) { }

  ngOnInit() {
  }

  public forgot(){
    this.authService.forgotpassword(this.model).subscribe(() => {
      this.alerts.onSuccess('Link do zmiany hasła został wysłany na adres email!');
      this.forgotBool = true;
    }, error => {
      this.errors = error;
      this.alerts.onError(error);
      console.log(error);
    });
  }
}
