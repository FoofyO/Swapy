import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ChatListService } from '../chat-list/chat-list.service';
import { DetailChatResponseDTO } from '../models/detail-chat-response-dto';
import { ChatApiService } from '../chat-api.service';
import { ChatDetailService } from './chat-detail.service';
import { AuthFacadeService } from '../../auth/services/auth-facade.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpStatusCode } from 'axios';

@Component({
  selector: 'app-chat-detail',
  templateUrl: './chat-detail.component.html',
  styleUrls: ['./chat-detail.component.scss']
})
export class ChatDetailComponent implements OnInit {
  myId!: string;
  selectedChat: DetailChatResponseDTO | null = null;
  inputTextToSend: string = '';
  productId: string | null = null;
  selectedFileToSend: File | undefined = undefined;
  @ViewChild('imageElement') previewImageElement!: ElementRef;

  constructor(private route: ActivatedRoute, private chatDetailService: ChatDetailService, private chatListService: ChatListService, private chatApiService: ChatApiService, private authFacadeService: AuthFacadeService, private router: Router) {
    chatDetailService.setChatDetailComponent(this);
    let userId = authFacadeService.getUserId()
    if(userId === null){ this.router.navigateByUrl('/404', { skipLocationChange: true }); return; }
    this.myId = userId;

  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.productId = params['productId'];
    });
    if(this.productId !== null) {
      this.chatApiService.getDetailChatByProductId(this.productId).subscribe(
        (response: DetailChatResponseDTO) => {
          this.selectedChat = response;
        },
        (error) => {
          if(error.response.status === HttpStatusCode.NotFound){
            if(this.productId !== null) {
              this.chatApiService.getTemporaryChat(this.productId).subscribe(
                (responseTemporaryChat: DetailChatResponseDTO) => {
                  this.selectedChat = responseTemporaryChat; 
                },
                error => {
                  this.productId = null;
                }
              )
            }
          }
        }
      );
    }
  }

  changeSelectedChat(chatId: string): void {
    this.chatApiService.getDetailChat(chatId).subscribe(
      (response: DetailChatResponseDTO) => { 
        this.selectedChat = response;
        this.productId = null;
      }
    );
  }

  sendMessage(): void {
    if(this.productId){
      this.chatApiService.CreateChatAsync(this.productId).subscribe(
        (response) => {
          ///this.chatListService.changeSelectedChat(response); // response.id
        }
      ) 
      this.productId = null; 
    }
    if(this.selectedChat !== null && this.inputTextToSend.trim().length >= 0){
      let formData = new FormData();
      formData.append("ChatId", this.selectedChat.chatId);
      formData.append("Text", this.inputTextToSend); 
      if(this.selectedFileToSend) { formData.append("image", this.selectedFileToSend); }
      this.chatApiService.SendMessageAsync(formData)
      this.inputTextToSend = '';
    }
  }

  selectFile(event: any): void {
    this.selectedFileToSend = event.target.files[0];
    if (this.selectedFileToSend) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        if (this.selectedFileToSend) {
          const arrayBuffer = e.target.result;
          const blob = new Blob([arrayBuffer], { type: this.selectedFileToSend.type });
          const imageUrl = URL.createObjectURL(blob);
          this.previewImageElement.nativeElement.src = imageUrl;
        }
      };
      reader.readAsArrayBuffer(this.selectedFileToSend);
    } 
    else {
      this.previewImageElement.nativeElement.src = 'undefined';
    }
  }

  backToList(): void {
    this.chatListService.toggleAnimation();
  }
}
