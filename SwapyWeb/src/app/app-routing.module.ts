<<<<<<< Updated upstream
import { NgModule } from '@angular/core';
import { AuthGuard } from './core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from './shared/error/error.component';
import { LoginComponent } from './modules/auth/components/login/login.component';

const routes: Routes = [
  {path: '', loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule)},
  {path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)},
  {path: 'users', loadChildren: () => import('./modules/users/users.module').then(m => m.UsersModule)},
  {path: 'shops', loadChildren: () => import('./modules/shops/shops.module').then(m => m.ShopsModule)},
  {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
=======
import { NgModule } from '@angular/core';
import { AuthGuard } from './core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from './shared/error/error.component';
import { LoginComponent } from './modules/auth/components/login/login.component';

const routes: Routes = [
  {path: '', loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule)},
  {path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)},
  {path: 'users', loadChildren: () => import('./modules/users/users.module').then(m => m.UsersModule)},
  {path: 'shops', loadChildren: () => import('./modules/shops/shops.module').then(m => m.ShopsModule)},
  {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
>>>>>>> Stashed changes
export class AppRoutingModule { }