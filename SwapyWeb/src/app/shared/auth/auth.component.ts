import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {

  constructor(public authFacade: AuthFacadeService, public router: Router) { }

  onLogout(): void {
    this.authFacade.logout();
    this.router.navigate(['/auth/login']);
  }
}
