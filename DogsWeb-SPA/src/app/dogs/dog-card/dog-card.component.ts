import { User } from '../../_models/user';
import { Component, Input, OnInit } from '@angular/core';
import { faEnvelope, faHeart, faPaw } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-dog-card',
  templateUrl: './dog-card.component.html',
  styleUrls: ['./dog-card.component.css']
})
export class DogCardComponent implements OnInit {

  @Input() user: User;
  faPaw = faPaw;
  faHeart = faHeart;
  faEnv = faEnvelope;
  constructor() { }

  ngOnInit() {
  }

}
