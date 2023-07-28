import { Injectable } from '@angular/core';
import { Language } from '../enums/language.enum';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  public get accessToken() : string {
    return localStorage.getItem('accessToken') as string;
  }

  public set accessToken(token : string) {
    localStorage.setItem('accessToken', token);
  }

  public removeToken() : void {
    localStorage.removeItem('accessToken');
  }

  ////////////////////////////////////////////////////////////

  public get userId() : string {
    return localStorage.getItem('userId') as string;
  }

  public set userId(id : string) {
    localStorage.setItem('userId', id);
  }

  public removeUserId() : void {
    localStorage.removeItem('userId');
  }

  ////////////////////////////////////////////////////////////

  public get userType(): string {
    return localStorage.getItem('userType') as string;
  }

  public set userType(type: string) {
    localStorage.setItem('userType', type);
  }

  public removeUserType() {
    localStorage.removeItem('userType');
  }

  ////////////////////////////////////////////////////////////

  public get localization() : Language {
    return localStorage.getItem('localization') as Language;
  }

  public set localization(type : Language) {
    localStorage.setItem('localization', type);
  }

  public removeLocalization() : void {
    localStorage.removeItem('localization');
  }

////////////////////////////////////////////////////////////

  public get rememberMe() : string {
    return localStorage.getItem('rememberMe') as string;
  }

  public set rememberMe(type : string) {
    localStorage.setItem('rememberMe', type);
  }

  public removeRememberMe() : void {
    localStorage.removeItem('rememberMe');
  }
}
