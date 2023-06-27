import * as $ from 'jquery';
import { Language } from 'src/app/core/enums/language';
import { Component, OnInit, HostListener } from '@angular/core';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})

export class HeaderComponent implements OnInit {
  oldScroll: number;
  buttonText: string;
  showElement : boolean;
  mediaQuery: MediaQueryList;
  mediaQueryCollapsed: MediaQueryList;

  constructor(private localStorage : LocalStorageService) { 
    this.oldScroll = 0;
    this.buttonText = '';
    this.showElement = false;
    this.mediaQuery = window.matchMedia('(min-width: 610px)');
    this.mediaQueryCollapsed = window.matchMedia('(min-width: 500px)');

    this.mediaQuery.addListener((event: MediaQueryListEvent) => {
      this.handleMediaQueryChange(event);
    });

    this.mediaQueryCollapsed.addListener((event: MediaQueryListEvent) => {
      this.handleMediaCollapsedQueryChange(event);
    });
  }

  ngOnInit(): void {
    switch (this.localStorage.localization) {
      case Language.English:
        this.buttonText = `EN`;
        break;
      case Language.Russian:
        this.buttonText = "RU";
        break;
      case Language.Azerbaijani:
        this.buttonText = "AZ";
        break;
      default:
        this.buttonText = "EN";
        break;
    }
  }

  //Window Width Change
  handleMediaQueryChange(event: MediaQueryListEvent) {
    if (event.matches) $('#navbarMenu').css('display', 'block');
    else $('#navbarMenu').css('display', 'none');
    this.showElement = false;
  }

  handleMediaCollapsedQueryChange(event: MediaQueryListEvent) {
    if (event.matches) $('#navbarCollapsedMenu').css('display', 'flex');
    else $('#navbarCollapsedMenu').css('display', 'none');
    this.showElement = false;
  }

  //Language Changing
  selectLanguage(language : string) : void {
    const selectedLanguage = Language[language as keyof typeof Language];
    if(selectedLanguage !== this.localStorage.localization) {
      this.localStorage.localization = Language[language as keyof typeof Language];
      window.location.reload();
    } 
  }

  //Toggle button
  toggleAnimation(argument : string) {
    this.showElement  = !this.showElement;
    const $navbarMenu = $(`#${argument}`);

    if(argument === 'navbarMenu') {
      if (this.showElement) {
        $navbarMenu.show()
        .addClass('slide-in-animation')
        .off('animationend')
        .on('animationend', function() {
          $navbarMenu.removeClass('slide-in-animation');
        });
      } 
      else {
        $navbarMenu.addClass('slide-out-animation')
        .off('animationend')
        .on('animationend', function() {
          $navbarMenu.removeClass('slide-out-animation');
          $navbarMenu.hide();
        });
      }
    } else {
      if (this.showElement) {
        $navbarMenu.css('display', 'flex')
        .addClass('slide-in-animation')
        .off('animationend')
        .on('animationend', function() {
          $navbarMenu.removeClass('slide-in-animation');
        });
      } 
      else {
        $navbarMenu.addClass('slide-out-animation')
        .off('animationend')
        .on('animationend', function() {
          $navbarMenu.css('display', 'none');
          $navbarMenu.removeClass('slide-out-animation');
        });
      }
    }
  }

  //Scroller
  @HostListener('window:scroll', [])
  onWindowScroll() {
    if (this.oldScroll > window.scrollY) {
      if ($('#header-navigation').hasClass("header-navigation-hide")) {
        $('#header-navigation').removeClass("header-navigation-hide");
      }
    } else {
      if (!$('#header-navigation').hasClass("header-navigation-hide")) {
        $('#header-navigation').addClass("header-navigation-hide");
        
        if (window.innerWidth <= 610) {
          const $navbarMenu = $('#navbarMenu');
          const $navbarCollapsedMenu = $('#navbarCollapsedMenu');

          if($navbarMenu.css('display') === "block") {
            this.showElement  = !this.showElement;
            $navbarMenu.addClass('slide-out-animation')
            .off('animationend')
            .on('animationend', function() {
              $navbarMenu.removeClass('slide-out-animation');
              $navbarMenu.hide();
            });
          }
          
          if($navbarCollapsedMenu.css('display') !== "none" ) {
            this.showElement  = !this.showElement;
            $navbarCollapsedMenu.addClass('slide-out-animation')
            .off('animationend')
            .on('animationend', function() {
              $navbarCollapsedMenu.removeClass('slide-out-animation');
              $navbarCollapsedMenu.hide();
            });
          }
        }
      }
    }
    this.oldScroll = window.scrollY;
  }
}