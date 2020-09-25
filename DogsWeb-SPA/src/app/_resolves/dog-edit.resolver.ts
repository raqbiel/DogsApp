import { AuthService } from '../_services/auth.service';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { AlertsService } from '../_services/alerts.service';
import { UserService } from '../_services/user.service';
import { User } from '../_models/user';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';


@Injectable()
export class DogEditResolver implements Resolve<User>{

  constructor(private userService: UserService, private router: Router, private alert: AlertsService, private authService: AuthService){

  }

  resolve(route: ActivatedRouteSnapshot): Observable<User>{
    return this.userService.getUser(this.authService.decodedToken.nameid).pipe(
      catchError(error => {
            this.alert.error('Problem z pobraniem Twoich danych');
            this.router.navigate(['/dopasowania']);
            return of(null);
      })
    );
  }
}
