import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guard/auth.guard';
import { AddContactsComponent } from './add-contacts/add-contacts.component';
import { ListContactsComponent } from './list-contacts/list-contacts.component';

const routes: Routes = [
  { path: '', component: ListContactsComponent, canActivate: [AuthGuard] },
  { path: 'add/:id', component: AddContactsComponent, canActivate: [AuthGuard] },
  { path: 'add', component: AddContactsComponent , canActivate: [AuthGuard] },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ContactsRoutingModule { }
