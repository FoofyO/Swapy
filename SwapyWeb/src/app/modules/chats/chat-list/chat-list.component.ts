import {ChangeDetectorRef, Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { ChatListService } from './chat-list.service';
import { SpinnerComponent } from 'src/app/shared/spinner/spinner.component';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { ChatApiService } from '../chat-api.service';
import { ChatListResponseDTO } from '../models/chat-list-response-dto';
import { ChatResponseDTO } from '../models/chat-response-dto';
import { ChatDetailService } from '../chat-detail/chat-detail.service';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  styleUrls: ['./chat-list.component.scss']
})
export class ChatListComponent implements OnInit  {
  showElement: boolean;
  isBuyersChats: boolean = true;
  chatList: ChatResponseDTO[] = [];
  selectedChat: ChatResponseDTO | null = null;

  constructor(private chatListService: ChatListService, private changeDetectorRef: ChangeDetectorRef, private chatDetailService: ChatDetailService, private chatApiService: ChatApiService, private spinnerService: SpinnerService, private elementRef: ElementRef, private renderer: Renderer2, private router: Router) {
    chatListService.setChatListComponent(this);
    this.showElement = false;
  }

  ngOnInit(): void {
    window.addEventListener('resize', this.handleWindowSizeChange.bind(this));
    this.handleWindowSizeChange();
    this.updateList();
  }

  updateList(): void {
    this.spinnerService.changeSpinnerState(true);
    (this.isBuyersChats ? this.chatApiService.getBuyersChats() : this.chatApiService.getSellersChats()).subscribe(
      (response : ChatListResponseDTO) => {
        this.chatList = response.items;
        this.spinnerService.changeSpinnerState(false);
      },
      (error) => {
        this.spinnerService.changeSpinnerState(false);
      }
    );
  }

  handleWindowSizeChange() {
    const chatList = this.elementRef.nativeElement.querySelector('.chat-list-container');

    if ((!(window.innerWidth > 860)) && this.selectedChat !== null) {
      this.showElement = false;
      this.renderer.addClass(chatList, 'slide-out-animation');
      this.renderer.listen(chatList, 'animationend', () => {
        this.renderer.removeClass(chatList, 'slide-out-animation');
        this.renderer.setStyle(chatList, 'display', 'none');
      });
    }
    else if(!this.showElement) {
      this.showElement = true;
      this.renderer.setStyle(chatList, 'display', 'grid');
      this.renderer.addClass(chatList, 'slide-in-animation');
      this.renderer.listen(chatList, 'animationend', () => {
        this.renderer.removeClass(chatList, 'slide-in-animation');
        this.renderer.setStyle(chatList, 'display', 'grid');
      });
    }
  }

  goToChat(newSelectedChat: ChatResponseDTO) {
    this.selectedChat = newSelectedChat;
    this.chatDetailService.changeSelectedChat(this.selectedChat.chatId);
  }

  switchChatList(isBuyersChats: boolean) {
    this.isBuyersChats = isBuyersChats;
    this.updateList();
  }

  changeSelectedChat(chatId: string){
    this.spinnerService.changeSpinnerState(true);
    (this.isBuyersChats ? this.chatApiService.getBuyersChats() : this.chatApiService.getSellersChats()).subscribe(
      (response : ChatListResponseDTO) => {
        this.chatList = response.items;
        let newSelectedChat = this.chatList.find(x => x.chatId === chatId);
        this.selectedChat = newSelectedChat ? newSelectedChat : this.selectedChat;
        this.spinnerService.changeSpinnerState(false);
      },
      (error) => {
        this.spinnerService.changeSpinnerState(false);
      }
    );
  }

  toggleAnimation() {
    if(window.innerWidth > 860) { return; }

    const chatList = this.elementRef.nativeElement.querySelector('.chat-list-container');

    if (!this.showElement) {
      this.showElement = !this.showElement;
      this.renderer.setStyle(chatList, 'display', 'grid');
      this.renderer.addClass(chatList, 'slide-in-animation');
      this.renderer.listen(chatList, 'animationend', () => {
        this.renderer.removeClass(chatList, 'slide-in-animation');
        this.renderer.setStyle(chatList, 'display', 'grid');
      });
    } 
    else if(this.selectedChat !== null) {
      this.showElement = !this.showElement;
      this.renderer.addClass(chatList, 'slide-out-animation');
      this.renderer.listen(chatList, 'animationend', () => {
        this.renderer.removeClass(chatList, 'slide-out-animation');
        this.renderer.setStyle(chatList, 'display', 'none');
      });
    }
  }
}
