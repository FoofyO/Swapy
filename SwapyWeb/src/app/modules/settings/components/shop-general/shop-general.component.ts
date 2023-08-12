import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';

@Component({
  selector: 'app-shop-general',
  templateUrl: './shop-general.component.html',
  styleUrls: ['./shop-general.component.scss']
})
export class ShopGeneralComponent {
  private inputFields = [
    { name: 'shopName', elementId: 'shopNameGroup' },
    { name: 'email', elementId: 'emailGroup' },
    { name: 'phoneNumber', elementId: 'phoneNumberGroup' },
  ];
  
  validEmail: boolean = false;
  showPassword: boolean = false;
  validShopName: boolean = false;
  validPhoneNumber: boolean = false;
  shopRegistratiomSuccess: boolean = false;

  shopGeneralForm: FormGroup;
  get shopName() { return this.shopGeneralForm.get('shopName'); }
  get email() { return this.shopGeneralForm.get('email'); }
  get phoneNumber() { return this.shopGeneralForm.get('phoneNumber'); }
  get password() { return this.shopGeneralForm.get('password'); }
  get rememberMe() { return this.shopGeneralForm.get('rememberMe'); }
  
  constructor(private authFacade: AuthFacadeService, private spinnerService: SpinnerService) {
    this.shopGeneralForm = new FormGroup({
      shopName: new FormControl(null, [Validators.required, Validators.pattern(`^([A-ZА-ЯƏÜÖĞİŞÇ]|[0-9])[A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç0-9\\s']{2,31}$`)]),
      email: new FormControl(null, [Validators.required, Validators.pattern('^[\\w\\-\\.]+@[\\w-]+\\.[a-z]{2,4}$')]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern('^\\+\\d{1,3}\\d{1,3}\\d{7}$')]),
    });
  }
  
  async onSubmit() : Promise<void> {
    try{
      if(this.shopGeneralForm.valid) {
        this.spinnerService.changeSpinnerState(true);
        await this.authFacade.shopRegistration(this.shopGeneralForm.value);
        this.spinnerService.changeSpinnerState(false);
        this.shopRegistratiomSuccess = true;
      }
    } catch (error : any) {
      this.spinnerService.changeSpinnerState(false);
      console.log(error);
    }
  }

  async onInputBlur(fieldName: string) : Promise<void> {
    const field = this.inputFields.find(f => f.name === fieldName);
    if (field) {
      if(field.name === 'email') {
        if(this.shopGeneralForm.get(fieldName)?.valid) {
          this.validEmail = await this.authFacade.checkEmailAvailability(this.email?.value);
          
          if(this.validEmail) {
            this.email?.setErrors({ 'emailNotAvailable': true });
            this.shopGeneralForm.setErrors({ 'invalidData': true });
          }
        }
        
        this.changeValidity(field.name, field.elementId);

      } else if (field.name === 'phoneNumber') {
        if(fieldName === 'phoneNumber' && this.shopGeneralForm.get(fieldName)?.valid) {
          this.validPhoneNumber = await this.authFacade.checkPhoneNumberAvailability(this.phoneNumber?.value);

          if(this.validPhoneNumber) {
            this.phoneNumber?.setErrors({ 'phoneNumberNotAvailable': true });
            this.shopGeneralForm.setErrors({ 'invalidData': true });
          }
        }

        this.changeValidity(field.name, field.elementId);

      } else if (field.name === 'shopName') {
        if(fieldName === 'shopName' && this.shopGeneralForm.get(fieldName)?.valid) {
          this.validShopName = await this.authFacade.checkShopNameAvailability(this.shopName?.value);

          if(this.validShopName) {
            this.shopName?.setErrors({ 'shopNameNotAvailable': true });
            this.shopGeneralForm.setErrors({ 'invalidData': true });
          }
        }

        this.changeValidity(field.name, field.elementId);

      } else this.changeValidity(field.name, field.elementId);
    }
  }

  changeValidity(fieldName: string, elementId: string) : void {
    const element = $(`#${elementId}`);

    if (this.shopGeneralForm.get(fieldName)?.valid) {
      element.removeClass('custom-invalid-card-group');
      element.addClass('custom-valid-card-group');
    } else {
      element.removeClass('custom-valid-card-group');
      element.addClass('custom-invalid-card-group');
    }
  }

}
