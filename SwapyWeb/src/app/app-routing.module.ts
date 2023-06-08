import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// import { AuthGuard } from './core/guards/auth-guard.guard';
// import { ErrorComponent } from './shared/components/error/error.component';

const routes: Routes = [
//   {path: '', redirectTo: '/account/login', pathMatch: 'full'},
//   {path: 'task', loadChildren: () => import('./modules/task/task.module').then(m=>m.TaskModule), canActivate: [AuthGuard]},
//   {path: 'folder', loadChildren: () => import('./modules/folder/folder.module').then(m=>m.FolderModule), canActivate: [AuthGuard]},
//   {path: 'account', loadChildren: () => import('./modules/account/account.module').then(m=>m.AccountModule)},
//   {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }