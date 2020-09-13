import { AlertsService } from '../../_services/alerts.service';
import { UserService } from '../../_services/user.service';
import { User } from '../../_models/user';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dog-details',
  templateUrl: './dog-details.component.html',
  styleUrls: ['./dog-details.component.scss']
})
export class DogDetailsComponent implements OnInit {

 user:User;


  constructor(private userService: UserService, private alert: AlertsService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadUser();
  }
  loadUser(){
    this.userService.getUser(this.route.snapshot.params['id']).subscribe((user: User) => {
      this.user = user;
    }, error =>{
      this.alert.onError(error);
    });
  }

}
