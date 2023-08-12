import { NgModule } from '@angular/core';
import { AuthGuard } from './core/guards/auth.guard';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from './shared/error/error.component';
import { ChatPanelComponent } from './modules/chats/chat-panel/chat-panel.component';

const routes: Routes = [
  {path: '', loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule)},
  {path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)},
  {path: 'chats', component: ChatPanelComponent}, //guard
  {path: 'settings', loadChildren: () => import('./modules/settings/settings.module').then(m => m.SettingsModule)}, //guard
  {path: 'shops', loadChildren: () => import('./modules/shops/shops.module').then(m => m.ShopsModule)},
  {path: 'users', loadChildren: () => import('./modules/users/users.module').then(m => m.UsersModule)},
  {path: '**', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }