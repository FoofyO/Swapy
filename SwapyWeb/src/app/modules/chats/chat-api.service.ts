import { Injectable } from '@angular/core';
import { AxiosError, AxiosResponse } from 'axios';
import { Observable, catchError, from, map } from 'rxjs';
import { Specification } from 'src/app/core/models/specification';
import { AxiosInterceptorService } from 'src/app/core/services/axios-interceptor.service';
import { environment } from 'src/environments/environment';
import { ChatListResponseDTO } from './models/chat-list-response-dto';
import { DetailChatResponseDTO } from './models/detail-chat-response-dto';

@Injectable({
  providedIn: 'root'
})
export class ChatApiService {

  private readonly chatsApiUrl: string = environment.chatsApiUrl;

  constructor(private axiosInterceptorService: AxiosInterceptorService) {}

  getBuyersChats(): Observable<ChatListResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.chatsApiUrl}/Chats/Buyers`)).pipe(
      map((response: AxiosResponse<any>) => {
        let chatListResponseDTO: ChatListResponseDTO = response.data;
        chatListResponseDTO.items.forEach(i => { i.logo = i.image; });
        return chatListResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getSellersChats(): Observable<ChatListResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.chatsApiUrl}/Chats/Sellers`)).pipe(
      map((response: AxiosResponse<any>) => {
        const chatListResponseDTO: ChatListResponseDTO = response.data;
        return chatListResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getDetailChat(chatId: string): Observable<DetailChatResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.chatsApiUrl}/Chats/${chatId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const detailChatResponseDTO: DetailChatResponseDTO = response.data;
        return detailChatResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getDetailChatByProductId(productId: string): Observable<DetailChatResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.chatsApiUrl}/ChatByProductId/${productId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const detailChatResponseDTO: DetailChatResponseDTO = response.data;
        return detailChatResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getTemporaryChat(productId: string): Observable<DetailChatResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.chatsApiUrl}/TemporaryChat/${productId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const detailChatResponseDTO: DetailChatResponseDTO = response.data;
        return detailChatResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  SendMessageAsync(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.chatsApiUrl}/Messages`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  CreateChatAsync(productId: string): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.chatsApiUrl}`, productId)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }
}


