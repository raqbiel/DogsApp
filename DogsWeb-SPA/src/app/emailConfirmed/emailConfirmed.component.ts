import { UserService } from './../_services/user.service';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { AlertsService } from '../_services/alerts.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-emailConfirmed',
  templateUrl: './emailConfirmed.component.html',
  styleUrls: ['./emailConfirmed.component.scss']
})
export class EmailConfirmedComponent implements OnInit {



  constructor(private alerts: AlertsService, private userService: UserService) { }

  ngOnInit() {

  }

}
