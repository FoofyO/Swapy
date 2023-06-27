import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthFacadeService } from '../../services/auth-facade.service';
import { Component, HostListener, OnInit} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  showFooter = false;
    
  get emailOrPhone() { return this.loginForm.get('emailOrPhone'); }
  get password() { return this.loginForm.get('password'); }

  constructor(private authFacade: AuthFacadeService, private router: Router) {
    this.loginForm = new FormGroup({
      emailOrPhone: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    })
  }
  
  async onSubmit() {
    try{
      //await this.authFacade.login(this.loginForm.value);
      this.router.navigate(['/folder/list']);
    } catch (error) {
      this.loginForm.reset();
      //let currentError = error as HttpErrorResponse;

      //if(currentError.status == 0) this.description = 'Server not found [404]';
      //else this.description = currentError.error;

      //this.isError = true;
    }
  }
}
