import { Component, OnInit, HostListener, Renderer2, ElementRef } from '@angular/core';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { CategoryTreeService } from 'src/app/shared/category-tree/category-tree.service';
import { Language } from 'src/app/core/enums/language.enum';
import { HeaderService } from './header.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductsSearchService } from 'src/app/modules/products/components/products-search/products-search.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  oldScroll: number;
  buttonText: string;
  showElement: boolean;

  searchQueryForm: FormGroup;
  get title() { return this.searchQueryForm.get('title'); }

  constructor(
    private renderer: Renderer2,
    private elementRef: ElementRef,
    private localStorage: LocalStorageService,
    private headerService: HeaderService,
    private categoryTreeService: CategoryTreeService,
    private route: ActivatedRoute,
    private router: Router,
    private productsSearchService: ProductsSearchService
  ) {
    this.oldScroll = 0;
    this.buttonText = '';
    this.showElement = false;

    this.route.queryParams.subscribe(params => {
      if (params['title']){
        this.searchQueryForm.setValue({
          title: params['title']
        });
      }
    });

    this.searchQueryForm = new FormGroup({
      title: new FormControl(null)
    });

    window.addEventListener('resize', () => {
      if(window.innerWidth > 840){
        const navbarMenu = this.elementRef.nativeElement.querySelector('#navbarMenu');
        this.waitForAnimationToEnd(navbarMenu, () => {
          this.renderer.setStyle(navbarMenu, 'display', 'flex');
          this.showElement = true;
        });
      }
      else{
        const navbarMenu = this.elementRef.nativeElement.querySelector('#navbarMenu');
        this.waitForAnimationToEnd(navbarMenu, () => {
          this.renderer.setStyle(navbarMenu, 'display', 'none');
          this.showElement = false;
        });
      }
    });
  }

  ngOnInit(): void {
    this.headerService.setHeaderComponent(this);
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

  selectLanguage(language: string): void {
    const selectedLanguage = Language[language as keyof typeof Language];
    if (selectedLanguage !== this.localStorage.localization) {
      this.localStorage.localization = Language[language as keyof typeof Language];
      window.location.reload();
    }
  }

  disableCollapsedMenuAnimation() {
    if(this.showElement){ 
      this.showElement = false;

      const headerOffnavElements = $('.header-offnav').toArray();

      headerOffnavElements.forEach((element) => {
        this.renderer.addClass(element, 'slide-out-animation');
        this.renderer.listen(element, 'animationend', () => {
          this.renderer.setStyle(element, 'display', 'none');
          this.renderer.removeClass(element, 'slide-out-animation');
        });
      });
    }
  }

  toggleCollapsedMenuAnimation(argument: string) {
    this.categoryTreeService.disableCategoryMenu();
    this.showElement = !this.showElement;
    const navbarMenu = this.elementRef.nativeElement.querySelector(`#${argument}`);

    if (this.showElement) {
      this.renderer.setStyle(navbarMenu, 'display', 'flex');
      this.renderer.addClass(navbarMenu, 'slide-in-animation');
      this.renderer.listen(navbarMenu, 'animationend', () => {
        this.renderer.removeClass(navbarMenu, 'slide-in-animation');
        this.renderer.setStyle(navbarMenu, 'display', 'flex');
      });
    } else {
      this.renderer.addClass(navbarMenu, 'slide-out-animation');
      this.renderer.listen(navbarMenu, 'animationend', () => {
        this.renderer.setStyle(navbarMenu, 'display', 'none');
        this.renderer.removeClass(navbarMenu, 'slide-out-animation');
      });
    }
  }

  toggleCategoryMenuAnimation() {
    this.categoryTreeService.toggleAnimation();
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const headerNavigation = this.elementRef.nativeElement.querySelector('#header-navigation');
    if (this.oldScroll > window.scrollY) {
      if (headerNavigation.classList.contains('header-navigation-hide')) {
        this.renderer.removeClass(headerNavigation, 'header-navigation-hide');
      }
    } else {
      if (!headerNavigation.classList.contains('header-navigation-hide')) {
        this.renderer.addClass(headerNavigation, 'header-navigation-hide');
      }
      if(this.showElement){ 
        this.showElement = false;
        
        const headerOffnavElements = $('.header-offnav').toArray();

        headerOffnavElements.forEach((element) => {
          this.renderer.addClass(element, 'slide-out-animation');
          this.renderer.listen(element, 'animationend', () => {
            this.renderer.setStyle(element, 'display', 'none');
            this.renderer.removeClass(element, 'slide-out-animation');
          });
        });
      }
    }

    this.oldScroll = window.scrollY;
  }

  waitForAnimationToEnd(element: HTMLElement, callback: () => void) {
    const animationName = window.getComputedStyle(element).animationName;
    if (animationName !== 'none') { element.addEventListener('animationend', callback, { once: true }); }
    else { callback(); }
  }

  onSearch(): void {
    if(this.searchQueryForm.valid) {
      if (window.location.href.includes('products/search')) {
        if (this.title?.value.trim().length !== 0) {
          this.productsSearchService.changeTitleFilter(this.title?.value);
        }
        else{
          this.productsSearchService.changeTitleFilter(null);
        }
      }
      else{
        if (this.title?.value.trim().length !== 0) {
          this.router.navigateByUrl('products/search?title=' + this.title?.value);
        }
      }
    }
  }
}