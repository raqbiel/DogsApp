import { ActivatedRoute } from '@angular/router';
import { User } from '../../_models/user';
import { AlertsService } from '../../_services/alerts.service';
import { UserService } from '../../_services/user.service';

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dogs-list',
  templateUrl: './dogs-list.component.html',
  styleUrls: ['./dogs-list.component.css']
})
export class DogsListComponent implements OnInit {

  users:User[];

  constructor(private userService: UserService, private alert: AlertsService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data =>{
      this.users = data['users'];
    });
  }

}
