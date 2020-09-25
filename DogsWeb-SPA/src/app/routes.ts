import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { DogEditResolver } from './_resolves/dog-edit.resolver';
import { DogEditComponent } from './dogs/dog-edit/dog-edit.component';
import { DogListResolver } from './_resolves/dog-list.resolver';
import { DogDetailResolver } from './_resolves/dog-detail.resolver';
import { DogDetailsComponent } from './dogs/dog-details/dog-details.component';
import { EmailConfirmedComponent } from './emailConfirmed/emailConfirmed.component';
import { AuthGuard } from './_guards/auth.guard';
import { ForgotPasswordComponent } from './forgotPassword/forgotPassword.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { DogListComponent } from './dogs/dog-list/dog-list.component';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent},
  {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        { path: 'lista', component: DogListComponent, resolve: { users: DogListResolver}},
        { path: 'lista/:id', component: DogDetailsComponent, resolve: { user: DogDetailResolver}},
        { path: 'dog/edit', component: DogEditComponent, resolve: { user: DogEditResolver}, canDeactivate: [PreventUnsavedChanges]},
        { path: 'wiadomosci', component: MessagesComponent},
        { path: 'dopasowania', component: ListsComponent},
      ]
  },
  { path: 'przypomnij', component: ForgotPasswordComponent},
  { path: 'emailconfirmed', component: EmailConfirmedComponent},
  { path: '**', redirectTo: '', pathMatch: 'full'},

];


