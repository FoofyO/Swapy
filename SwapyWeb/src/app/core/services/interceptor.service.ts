import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { LocalStorageService } from './local-storage.service';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor(private localStorage: LocalStorageService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let requestWithToken = this.addAccessToken(request);
    let requestWithLocalization = this.addLocalization(requestWithToken);
    return next.handle(requestWithLocalization);
  }

  addAccessToken(request: HttpRequest<any>) : HttpRequest<any> {
    if (this.localStorage.accessToken)
      return request.clone({setHeaders: { 'Authorization': `Bearer ${this.localStorage.accessToken}` }});
    else
      return request;
  }

  addLocalization(request: HttpRequest<any>): HttpRequest<any> {
    return request.clone({
      setHeaders: { 'Localization': this.localStorage.localization }
    });
  }
}