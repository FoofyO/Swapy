import { Injectable } from '@angular/core';
import axios, { AxiosError, AxiosResponse } from 'axios';
import { EMPTY, Observable, catchError, from, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserDetail } from '../models/user-detail.interface';


@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  private readonly usersApiUrl : string = environment.usersApiUrl;

  getUserById(id: string): Observable<UserDetail> {
    return from(axios.get(`${this.usersApiUrl}/${id}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const userDetail: UserDetail = response.data;
        return userDetail;
      }),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  checkLike(userId: string): Observable<boolean> {
    return from(axios.get(`${this.usersApiUrl}/Likes/Check/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const isLike: boolean = response.data;
        return isLike;
      }),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  checkSubscription(userId: string): Observable<boolean> {
    return from(axios.get(`${this.usersApiUrl}/Subscriptions/Check/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const isSubscription: boolean = response.data;
        return isSubscription;
      }),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  addLike(userId: string): Observable<void> {
    return from(axios.post(`${this.usersApiUrl}/Likes/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {}),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  addSubscription(userId: string): Observable<void> {
    return from(axios.post(`${this.usersApiUrl}/Subscriptions/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {}),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  removeLike(userId: string): Observable<void> {
    return from(axios.delete(`${this.usersApiUrl}/Likes/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {}),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  removeSubscription(userId: string): Observable<void> {
    return from(axios.delete(`${this.usersApiUrl}/Subscriptions/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {}),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

}