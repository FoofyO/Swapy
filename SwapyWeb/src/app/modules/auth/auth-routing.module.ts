import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { ShopRegisterComponent } from './components/shop-register/shop-register.component';
import { UserRegisterComponent } from './components/user-register/user-register.component';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'login', component: LoginComponent }, //canActivate: [DeauthGuard]},
  {path: 'register/user', component: UserRegisterComponent }, //canActivate: [DeauthGuard]},
  {path: 'register/shop', component: ShopRegisterComponent } //canActivate: [DeauthGuard]},
//{path: 'register', component: RegisterComponent, canActivate: [DeauthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }