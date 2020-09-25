import { AlertsService } from './../_services/alerts.service';
import { AuthService } from './../_services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alerts: AlertsService){}
  canActivate(): boolean {
   if(this.authService.loggedIn()){
     return true;
   }
   this.alerts.onError('Zaloguj się, aby wyświetlić stronę');
   this.router.navigate(['/home']);
   return false;

  }

}
