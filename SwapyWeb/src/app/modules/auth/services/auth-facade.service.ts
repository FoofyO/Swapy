import { Injectable } from '@angular/core';
import { AuthApiService } from './auth-api.service';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthFacadeService {

  constructor(private authApi: AuthApiService, private localStorage: LocalStorageService) { }

  isAuthenticated(): Boolean {
    if(this.localStorage.accessToken) return true;
    return false;
  }

  // async register(credential: AccountCredential) {
  //   await this.authApi.register(credential);
  // }

  // async login(credential: AccountCredential) {
  //   let response = await this.authApi.login(credential);
  //   this.localStorage.accessToken = response[1];
  //   this.localStorage.userType = response[2];
  //   this.localStorage.userId = response[3];
  // }

  logout() {
    this.localStorage.removeToken();
    this.localStorage.removeUserId();
    this.localStorage.removeUserType();
  }
}
