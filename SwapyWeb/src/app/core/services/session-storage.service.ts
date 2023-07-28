import { Injectable } from '@angular/core';
import { Language } from '../enums/language.enum';
import { UserType } from '../enums/user-type.enum';

@Injectable({
  providedIn: 'root'
})
export class SessionStorageService {

  constructor() { }

  public get accessToken(): string {
    return sessionStorage.getItem('accessToken') as string;
  }

  public set accessToken(token: string) {
    sessionStorage.setItem('accessToken', token);
  }

  public removeToken() {
    sessionStorage.removeItem('accessToken');
  }

  ////////////////////////////////////////////////////////////

  public get userId(): string {
    return sessionStorage.getItem('userId') as string;
  }

  public set userId(id: string) {
    sessionStorage.setItem('userId', id);
  }

  public removeUserId() {
    sessionStorage.removeItem('userId');
  }

  ////////////////////////////////////////////////////////////

  public get userType(): string {
    return sessionStorage.getItem('userType') as string;
  }

  public set userType(type: string) {
    sessionStorage.setItem('userType', type);
  }

  public removeUserType() {
    sessionStorage.removeItem('userType');
  }

  ////////////////////////////////////////////////////////////

  public get localization(): Language {
    return sessionStorage.getItem('localization') as Language;
  }

  public set localization(type: Language) {
    sessionStorage.setItem('localization', type);
  }

  public removeLocalization() {
    sessionStorage.removeItem('localization');
  }
}