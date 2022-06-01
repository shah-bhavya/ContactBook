import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChangeBgColorDirective } from 'src/app/shared/directive/change-bg-color.directive';
import { HighlightDirective } from 'src/app/shared/directive/highlight.directive';


@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent,
   ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class AccountModule { }
