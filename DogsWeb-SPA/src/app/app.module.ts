import { appRoutes } from './routes';
import { Routes, RouterModule } from '@angular/router';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AuthService } from './_services/auth.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

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
import { ResetPasswordComponent } from './resetPassword/resetPassword.component';
import { ForgotPasswordComponent } from './forgotPassword/forgotPassword.component';
import { DogsListComponent } from './dogs-list/dogs-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    RegisterComponent,
      ResetPasswordComponent,
      ForgotPasswordComponent,
      DogsListComponent,
      ListsComponent,
      MessagesComponent
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
    BsDropdownModule.forRoot(),
    SimpleNotificationsModule.forRoot(),
    PushNotificationsModule,
    RouterModule.forRoot(appRoutes)

  ],
  providers: [
    ErrorInterceptorProvider,



  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
