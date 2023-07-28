import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatDetailComponent } from './chat-detail/chat-detail.component';
import { ChatListComponent } from './chat-list/chat-list.component';
import { ChatPanelComponent } from './chat-panel/chat-panel.component';



@NgModule({
  declarations: [
    ChatDetailComponent,
    ChatListComponent,
    ChatPanelComponent
  ],
  imports: [
    CommonModule
  ]
})
export class ChatsModule { }
