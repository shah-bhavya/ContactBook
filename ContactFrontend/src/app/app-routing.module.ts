import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListContactsComponent } from './modules/contacts/list-contacts/list-contacts.component';

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
