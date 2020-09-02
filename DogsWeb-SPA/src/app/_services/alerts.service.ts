import { Injectable } from '@angular/core';
import * as alert from 'alertifyjs';
import { NotificationsService } from 'angular2-notifications';

@Injectable({
  providedIn: 'root'
})
export class AlertsService {

constructor( private service: NotificationsService) { }

  confirm(message: string, okCallback: () => any){
    alert.confirm(message,(e: any) => {
      if(e){
        okCallback();
      }else{}
    });
  }

  // success(message: string){
  //   alert.success(message);
  // }

   error(message: string){
    alert.error(message);
   }

  // warning(message: string){
  //   alert.warning(message);
  // }

  // message(message: string){
  //   alert.message(message);
  // }

  onSuccess(message){
    this.service.success('Sukces', message, {
      position: ['bottom', 'right'],
      timeOut: 2000,
      animate: 'fade',
      showProgressBar: true
    });
  }

  onError(message){
    this.service.error('Błąd', message, {
      position: ['bottom', 'right'],
      timeOut: 2000,
      animate: 'fade',
      showProgressBar: true
    });
  }

  onInfo(message){
    this.service.info('Informacja', message, {
      position: ['bottom', 'right'],
      timeOut: 2000,
      animate: 'fade',
      showProgressBar: true
    });
  }


}
