import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title: string = 'SwapyWeb';
  IsShowHeader: boolean = true;
  IsShowFooter: boolean = true;

  constructor(private router: Router) {
    this.router.events.subscribe(event => this.onShowUI(event));
  }

  onShowUI(location: any): void {
    if (location instanceof NavigationEnd) {
      if (location.url === '/') this.isShowUI(true);
      else if (location.url.includes('/chats')) this.isShowUI(true);
      else if (location.url.includes('/products/add')) this.isShowUI(false);
      else if (location.url.includes('/products/edit')) this.isShowUI(false);
      else if (location.url.includes('/products')) this.isShowUI(true);
      else if (location.url.includes('/settings')) this.showUIWithOutFooter();
      else if (location.url.includes('/shops')) this.isShowUI(true);
      else if (location.url.includes('/users')) this.isShowUI(true);
      else this.isShowUI(false);
    }
  }

  isShowUI(value: boolean): void { this.IsShowHeader = this.IsShowFooter = value; }

  showUIWithOutFooter(): void {
    this.IsShowHeader = true;
    this.IsShowFooter = false;
  }

  showUIWithOutHeader(): void {
    this.IsShowFooter = true;
    this.IsShowHeader = false;
  }
}