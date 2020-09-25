import { AuthService } from './../../_services/auth.service';
import { UserService } from './../../_services/user.service';
import { AlertsService } from './../../_services/alerts.service';
import { User } from '../../_models/user';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faEnvelope, faHeart, faUserCheck } from '@fortawesome/free-solid-svg-icons';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-dog-edit',
  templateUrl: './dog-edit.component.html',
  styleUrls: ['./dog-edit.component.css']
})
export class DogEditComponent implements OnInit {
  @ViewChild('editForm',{static: true}) editForm: NgForm;
  user: User;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  faHeart = faHeart;
  faEnv = faEnvelope;
  faCheck = faUserCheck;

  constructor(private route: ActivatedRoute, private alerts: AlertsService, private userService: UserService, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
      console.log(this.user);
    });
  }
  updateUser(){
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.alerts.onSuccess('Profil został zaaktualizowany!');
      this.editForm.reset(this.user);
    }, error =>{
      this.alerts.onError(error);
    });


  }

}
