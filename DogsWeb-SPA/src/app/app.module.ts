import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { UserService } from './_services/user.service';
import { AlertsService } from './_services/alerts.service';

import { appRoutes } from './routes';
import { RouterModule } from '@angular/router';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { SimpleNotificationsModule } from 'angular2-notifications';
import { PushNotificationsModule } from 'ng-push-ivy';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AuthGuard } from './_guards/auth.guard';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';

import { DogListResolver } from './_resolves/dog-list.resolver';
import { DogDetailResolver } from './_resolves/dog-detail.resolver';
import { DogEditResolver } from './_resolves/dog-edit.resolver';

import { DogEditComponent } from './dogs/dog-edit/dog-edit.component';
import { DogDetailsComponent } from './dogs/dog-details/dog-details.component';
import { DogCardComponent } from './dogs/dog-card/dog-card.component';
import { ResetPasswordComponent } from './resetPassword/resetPassword.component';
import { ForgotPasswordComponent } from './forgotPassword/forgotPassword.component';
import { DogListComponent } from './dogs/dog-list/dog-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { EmailConfirmedComponent } from './emailConfirmed/emailConfirmed.component';

export function tokenGetter(){
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
    ResetPasswordComponent,
    ForgotPasswordComponent,
    DogListComponent,
    ListsComponent,
    MessagesComponent,
    EmailConfirmedComponent,
    DogCardComponent,
    DogDetailsComponent,
    DogEditComponent
   ],
  imports: [
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    BrowserModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    TabsModule.forRoot(),
    BsDropdownModule.forRoot(),
    SimpleNotificationsModule.forRoot(),
    PushNotificationsModule,
    RouterModule.forRoot(appRoutes),
    FontAwesomeModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/auth']
      }
    }),
    NgxGalleryModule
  ],
  providers: [
    ErrorInterceptorProvider,
    AlertsService,
    AuthGuard,
    PreventUnsavedChanges,
    UserService,
    DogDetailResolver,
    DogListResolver,
    DogEditResolver



  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
