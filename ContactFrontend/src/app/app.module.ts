import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { AccountService } from './core/services/account/account.service';
import { AuthService } from './core/services/auth/auth.service';
import { ContactService } from './core/services/contact/contact.service';
import { PopupService } from './core/services/contact/popup.service';
import { AccountModule } from './modules/account/account.module';
import { ContactsModule } from './modules/contacts/contacts.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ContactsModule,
    AccountModule,
    CoreModule,
    HttpClientModule,
    Ng2SearchPipeModule,
    BrowserAnimationsModule
  ],
  providers: [
    ContactService,
    AccountService,
    AuthService,
    PopupService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
