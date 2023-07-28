import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { SessionStorageService } from './session-storage.service';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(private localStorage: LocalStorageService, private sessionStorage: SessionStorageService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let requestWithToken = this.addAccessToken(request);
    let requestWithLocalization = this.addLocalization(requestWithToken);
    return next.handle(requestWithLocalization);
  }

  addAccessToken(request: HttpRequest<any>) : HttpRequest<any> {
    if(this.localStorage.rememberMe) {
      if(this.localStorage.rememberMe === "true") {
        if (this.localStorage.accessToken) {
          return request.clone({setHeaders: { 'Authorization': `Bearer ${this.localStorage.accessToken}` }});
        }
      } else {
        if(this.sessionStorage.accessToken) {
          return request.clone({setHeaders: { 'Authorization': `Bearer ${this.sessionStorage.accessToken}` }});
        }
      }
    }
    return request;
  }

  addLocalization(request: HttpRequest<any>): HttpRequest<any> {
    return request.clone({
      setHeaders: { 'Localization': this.localStorage.localization }
    });
  }
}