import { DogDetailsComponent } from './dogs/dog-details/dog-details.component';
import { EmailConfirmedComponent } from './emailConfirmed/emailConfirmed.component';
import { AuthGuard } from './_guards/auth.guard';
import { ForgotPasswordComponent } from './forgotPassword/forgotPassword.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { DogsListComponent } from './dogs/dogs-list/dogs-list.component';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent},
  {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        { path: 'dopasowania', component: DogsListComponent},
        { path: 'dopasowania/:id', component: DogDetailsComponent},
        { path: 'wiadomosci', component: MessagesComponent},
        { path: 'lista-uzytkownikow', component: ListsComponent},
      ]
  },
  { path: 'przypomnij', component: ForgotPasswordComponent},
  { path: 'emailconfirmed', component: EmailConfirmedComponent},
  { path: '**', redirectTo: '', pathMatch: 'full'},

];


