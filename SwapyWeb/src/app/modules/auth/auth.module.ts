import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { ShopRegisterComponent } from './components/shop-register/shop-register.component';
import { UserRegisterComponent } from './components/user-register/user-register.component';

@NgModule({
  declarations: [
    LoginComponent,
    ShopRegisterComponent,
    UserRegisterComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    HttpClientModule,
    AuthRoutingModule,
    ReactiveFormsModule
  ],
})
export class AuthModule { }