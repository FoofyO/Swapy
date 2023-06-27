import { NgModule } from '@angular/core';
import { AuthGuard } from './core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from './shared/error/error.component';
import { LoginComponent } from './modules/auth/components/login/login.component';

const routes: Routes = [
  {path: '', redirectTo: '/auth/login', pathMatch: 'full'},
  {path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)},
//   {path: 'folder', loadChildren: () => import('./modules/folder/folder.module').then(m=>m.FolderModule), canActivate: [AuthGuard]},
//   {path: 'account', loadChildren: () => import('./modules/account/account.module').then(m=>m.AccountModule)},
  {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }