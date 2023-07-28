import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title: string = 'SwapyWeb';
  IsLoadingUI: boolean = false;
  IsShowUI: boolean = false;

  constructor(private router: Router) {
    this.router.events.subscribe(event => this.onShowUI(event));
  }

  onShowUI(location: any): void {
    this.IsLoadingUI = true;
    if (location instanceof NavigationEnd) {
      if (location.url === '/') this.IsShowUI = true;
      else if (location.url.includes('/shops')) this.IsShowUI = true;
      else if (location.url.includes('/users')) this.IsShowUI = true;
      else this.IsShowUI = false;
    }
    this.IsLoadingUI = false;
  }
}