import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContactsRoutingModule } from './contacts-routing.module';
import { ListContactsComponent } from './list-contacts/list-contacts.component';
import { AddContactsComponent } from './add-contacts/add-contacts.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchFilterPipe } from 'src/app/shared/pipe/search-filter.pipe';
import { NgxPaginationModule } from 'ngx-pagination';
import { TextTransformPipe } from 'src/app/shared/pipe/text-transform.pipe';
import { FavouriteContactsComponent } from './favourite-contacts/favourite-contacts.component';
import { ChangeBgColorDirective } from 'src/app/shared/directive/change-bg-color.directive';
import { PopUpAddComponent } from './popup-add-contact/popup-add-contact.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
@NgModule({
  declarations: [
    ListContactsComponent,
    AddContactsComponent,
    SearchFilterPipe,
    TextTransformPipe,
    FavouriteContactsComponent,
    ChangeBgColorDirective,
    PopUpAddComponent
  ],
  imports: [
    CommonModule,
    ContactsRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
    BrowserAnimationsModule
  ]
})
export class ContactsModule { }
