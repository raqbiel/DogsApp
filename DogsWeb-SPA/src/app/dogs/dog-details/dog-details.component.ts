import { AlertsService } from '../../_services/alerts.service';
import { UserService } from '../../_services/user.service';
import { User } from '../../_models/user';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TabsModule } from 'ngx-bootstrap/tabs/';
import { faEnvelope, faHeart } from '@fortawesome/free-solid-svg-icons';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-dog-details',
  templateUrl: './dog-details.component.html',
  styleUrls: ['./dog-details.component.css']
})
export class DogDetailsComponent implements OnInit {

 user:User;

 galleryOptions: NgxGalleryOptions[];
 galleryImages: NgxGalleryImage[];

 faHeart = faHeart;
 faEnv = faEnvelope;

  constructor(private userService: UserService, private alert: AlertsService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data =>{
      this.user = data['user'];
    });

    this.galleryOptions = [
      {
        width: '400px',
        height: '400px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      },
      // max-width 800
      {
        breakpoint: 800,
        width: '100%',
        height: '500px',
        imagePercent: 80,
        thumbnailsPercent: 20,
        thumbnailsMargin: 20,
        thumbnailMargin: 20
      },
    ];

    this.galleryImages = this.getImages();

  }
 getImages() {
   const imageUrls = [];
   for (const photo of this.user.photos){
     imageUrls.push({
      small: photo.url,
      medium: photo.url,
      big: photo.url,
      descsription: photo.description
     });
   }
   return imageUrls;
 }
}
