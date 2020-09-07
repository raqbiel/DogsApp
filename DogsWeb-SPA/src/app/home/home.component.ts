import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  passwReseted = false;
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(registerMode: boolean){
    this.registerMode = registerMode;
  }

  loggedIn(){
    return this.authService.loggedIn();
   }


}
