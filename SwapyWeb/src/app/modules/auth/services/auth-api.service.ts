import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginCredential, UserRegistrationCredential, ShopRegistrationCredential, AuthResponse, ResetPasswordCredential, EmailVerifyCredential } from '../models/auth-credentials';
import axios, { AxiosResponse } from 'axios';
import { catchError, from, map, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {

  private readonly apiUrl: string = environment.authApiUrl;

  constructor() { }

  login(credential: LoginCredential): Observable<AuthResponse> {
    return from(axios.post(`${this.apiUrl}/Login`, credential))
           .pipe(
              map((response: AxiosResponse<AuthResponse>) => ({
                type: response.data.type,
                userId: response.data.userId,
                accessToken: response.data.accessToken,
                refreshToken: response.data.refreshToken
              })),
              catchError(error => { throw error; }));
  }

  userRegistration(credential: UserRegistrationCredential): Observable<any> {
    return from(axios.post(`${this.apiUrl}/Register/User`, credential))
           .pipe(
            catchError(error => { throw error; })
           );
  }

  shopRegistration(credential: ShopRegistrationCredential): Observable<any> {
    return from(axios.post(`${this.apiUrl}/Register/Shop`, credential))
           .pipe(
            catchError(error => { throw error; })
           );
  }

  checkEmailAvailability(email: string): Observable<boolean> {
    return from(axios.get(`${this.apiUrl}/Check?Email=${encodeURIComponent(email)}`))
          .pipe(
            map((response: AxiosResponse<any>) => { 
              const result: boolean = response.data;
              return result; 
            }),
            catchError(error => { throw error; })
    );
  }

  checkPhoneNumberAvailability(phoneNumber: string): Observable<boolean> {
    return from(axios.get(`${this.apiUrl}/Check?PhoneNumber=${encodeURIComponent(phoneNumber)}`))
          .pipe(
            map((response: AxiosResponse<any>) => { 
              const result: boolean = response.data;
              return result; 
            }),
            catchError(error => { throw error; })
    );
  }

  checkShopNameAvailability(shopName: string): Observable<boolean> {
    return from(axios.get(`${this.apiUrl}/Check?ShopName=${encodeURIComponent(shopName)}`))
          .pipe(
            map((response: AxiosResponse<any>) => { 
              const result: boolean = response.data;
              return result; 
            }),
            catchError(error => { throw error; })
    );
  }

  forgotPassword(email: string) : Observable<any> {
    return from(axios.post(`${this.apiUrl}/ForgotPassword`, email))
           .pipe(
            catchError(error => { throw error; })
           );
  }

  resetPassword(credential: ResetPasswordCredential) : Observable<any> {
    return from(axios.patch(`${this.apiUrl}/ResetPassword`, credential))
            .pipe(
            catchError(error => { throw error; })
            );
  }

  emailVerify(credential: EmailVerifyCredential) : Observable<any> {
    return from(axios.patch(`${this.apiUrl}/ConfirmEmail`, credential))
           .pipe(
            catchError(error => { throw error; })
           );
  }

  async logout(): Promise<void>
  {
    const url = `${this.apiUrl}/Logout}`;
    await axios.get(url);
  }
}