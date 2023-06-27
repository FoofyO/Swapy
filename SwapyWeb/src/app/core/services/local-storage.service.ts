import { Injectable } from '@angular/core';
import { Language } from '../enums/language';
import { UserType } from '../enums/user-type';

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

  public removeToken() {
    localStorage.removeItem('accessToken');
  }

  ////////////////////////////////////////////////////////////

  public get userId() : string {
    return localStorage.getItem('userId') as string;
  }

  public set userId(id : string) {
    localStorage.setItem('userId', id);
  }

  public removeUserId() {
    localStorage.removeItem('userId');
  }

  ////////////////////////////////////////////////////////////

  public get userType() : UserType {
    return localStorage.getItem('userType') as UserType;
  }

  public set userType(type : UserType) {
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

  public removeLocalization() {
    localStorage.removeItem('localization');
  }
}
