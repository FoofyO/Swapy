import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AxiosResponse } from 'axios';
import { catchError, from, map, Observable, tap } from 'rxjs';
import { AxiosInterceptorService } from 'src/app/core/services/axios-interceptor.service';
import { UserData } from '../models/user-data';

@Injectable({
  providedIn: 'root'
})
export class SettingsApiService {

  private readonly usersApiUrl: string = environment.usersApiUrl;
  private readonly shopsApiUrl: string = environment.shopsApiUrl;

  constructor(private axiosInterceptorService: AxiosInterceptorService) {}

  getUserData(): Observable<UserData> {
    return from(this.axiosInterceptorService.get<any>(`${this.usersApiUrl}/UserData`))
           .pipe(
            tap((response: AxiosResponse<any>) => {
              console.log(response.data); // Вывести данные в консоль для отладки
          }),
              map((response: AxiosResponse<UserData>) => ({
                firstName: response.data.firstName,
                lastName: response.data.lastName,
                phoneNumber: response.data.phoneNumber,
                logo: response.data.logo,
                isSubscribed: response.data.isSubscribed
              })),
            catchError(error => { throw error; }));
  }
}