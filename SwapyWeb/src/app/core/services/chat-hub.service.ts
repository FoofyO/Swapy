import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ChatMessageModel } from 'src/app/modules/chats/models/chat-message.model';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { ChatDetailService } from 'src/app/modules/chats/chat-detail/chat-detail.service';
import { ChatListService } from 'src/app/modules/chats/chat-list/chat-list.service';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private userId: string;
  private hubConnection: signalR.HubConnection;

  constructor(private authFacade: AuthFacadeService, private chatDetail: ChatDetailService, private chatList: ChatListService) {
    var temp =  this.authFacade.getUserId();
    this.userId = temp == null ? "" : temp ;

    this.hubConnection = new signalR.HubConnectionBuilder()
        .configureLogging(signalR.LogLevel.Debug)
        .withUrl("https://localhost:7083/chatHub", {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
            accessTokenFactory: () => this.userId
        })
    .build();
  }

  startConnection() {
    this.hubConnection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  sendMessage(model: string) {
    this.hubConnection.invoke('SendMessage', model)
      .catch(err => console.error(err));
  }

  receiveMessages() {
    this.hubConnection.on('ReceiveMessage', (text: ChatMessageModel) => {
        this.chatDetail.receiveMessage(text);
        this.chatList.receiveMessage(text);
    });
  }
}