import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { ErrorComponent } from './shared/error/error.component';
import { LoginComponent } from './modules/auth/components/login/login.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title: string = 'SwapyWeb';
  IsLoadingUI: boolean = false;
  IsShowUI: boolean = false;

  constructor(private router: Router) {
  }

  ngOnInit(): void {
    this.IsLoadingUI = true;
    
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        if(event.url.includes('/auth')) this.IsShowUI = false;
        //else if(event.url.includes('/auth')) this.showFooter = false;
        else this.IsShowUI = true;
      }
    });

    this.IsLoadingUI = false;
  }
}
