import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SwapyHubService {
  private userId!: string;
  private hubConnection!: signalR.HubConnection;

  constructor(private authFacade: AuthFacadeService) {
    if(!this.isConnected()) {
        var temp =  this.authFacade.getUserId();
        this.userId = temp == null ? "" : temp ;

        if(this.userId !== "") {
            this.hubConnection = new signalR.HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.None)
            .withUrl(`${environment.apiUrl}/swapyHub`, {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets,
                accessTokenFactory: () => this.userId
            })
            .build();
        }
    }
  }

  configureHubConnection() {
    var temp =  this.authFacade.getUserId();
    this.userId = temp == null ? "" : temp ;

    if(this.userId !== "") {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.Debug)
            .withUrl(`${environment.apiUrl}/swapyHub`, {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets,
                accessTokenFactory: () => this.userId
            })
        .build();
    }

    this.startConnection();
  }

  startConnection() {
    if(this.userId !== "") {
        this.hubConnection.start()
                          .then(() => console.log('Connection started'))
                          .catch(err => console.log('Error while starting connection: ' + err));
    }
  }

  isConnected(): boolean {
    return this.hubConnection?.state === signalR.HubConnectionState.Connected;
  }

  disconnect() {
    if (this.isConnected()) {
      this.hubConnection.stop()
                        .then(() => console.log('Connection stopped'))
                        .catch(err => console.error('Error while stopping connection: ' + err));
    }
  }
}