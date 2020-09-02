import { AlertsService } from './../_services/alerts.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  errors: any = {};

  constructor(private authService: AuthService, private alerts: AlertsService) { }

  ngOnInit() {
  }

  register(){
    this.authService.register(this.model).subscribe(() => {
      this.alerts.onSuccess('Rejestracja zakończona sukcesem!');
    }, error => {
      this.errors = error;
      this.alerts.onError(error);
      console.log(error);
    });
  }

  cancel(){
    this.cancelRegister.emit(false);
    console.log('Canceled');
  }

}
