import { Router } from '@angular/router';
import { Component, Renderer2 } from '@angular/core';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { CategoryTreeService } from 'src/app/shared/category-tree/category-tree.service';
import { HeaderService } from '../header/header.service';
import { UserType } from 'src/app/core/enums/user-type.enum';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {

  constructor(public authFacade: AuthFacadeService, public router: Router, private renderer: Renderer2, private headerService: HeaderService,private categoryTreeService: CategoryTreeService) { }

  async onLogout(): Promise<void> {
    await this.authFacade.logout();
    window.location.replace("/");
  }

  toggleCategoryMenuAnimation(){
    this.headerService.disableCollapsedMenu();
    this.categoryTreeService.toggleAnimation();
  }

  transferToProfile(): void{
    this.router.navigate([this.authFacade.isAuthenticated() ? ('/' + (this.authFacade.getUserType() === UserType.Shop ? 'shops' : 'users') + '/' + this.authFacade.getUserId()) : '/auth/login']);
  }

  transferToFavorites(): void{
    this.router.navigate(['/products/favorites']);
  }
  
  transferToAddProduct(): void{
    this.router.navigate(['/products/add']);
  }

  transferToLogin(): void{
    this.router.navigate(['/auth/login']);
  }

  transferToChats(): void{
    this.router.navigate(['/chats']);
  }
}
