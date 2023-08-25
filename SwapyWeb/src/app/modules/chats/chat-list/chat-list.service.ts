import { Injectable } from '@angular/core';
import { ChatListComponent } from './chat-list.component';

@Injectable({
  providedIn: 'root'
})
export class ChatListService {
  private chatListComponent: ChatListComponent | null = null;

  setChatListComponent(component: ChatListComponent) {
    this.chatListComponent = component;
  }

  switchChatList(isBuyersChats: boolean) {
    if (this.chatListComponent) {
        this.chatListComponent.switchChatList(isBuyersChats);
    }
  }

  updateList() {
    if (this.chatListComponent) {
      this.chatListComponent.updateList();
    }
  }

  changeSelectedChat(chatId: string) {
    if (this.chatListComponent) {
      this.chatListComponent.changeSelectedChat(chatId);
    }
  }

  toggleAnimation() {
    if (this.chatListComponent) {
      this.chatListComponent.toggleAnimation();
    }
  }
}