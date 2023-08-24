import { Injectable } from '@angular/core';
import { ChatDetailComponent } from './chat-detail.component';

@Injectable({
  providedIn: 'root'
})
export class ChatDetailService {
  private chatDetailComponent: ChatDetailComponent | null = null;

  setChatDetailComponent(component: ChatDetailComponent) {
    this.chatDetailComponent = component;
  }

  changeSelectedChat(chatId: string) {
    if (this.chatDetailComponent) {
        this.chatDetailComponent.changeSelectedChat(chatId);
    }
  }
}