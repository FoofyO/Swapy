import { FormControl, FormGroup, Validators} from '@angular/forms';
import { Component} from '@angular/core';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';


@Component({
  selector: 'app-user-general',
  templateUrl: './user-general.component.html',
  styleUrls: ['./user-general.component.scss']
})
export class UserGeneralComponent {
  private inputFields = [
    { name: 'firstName', elementId: 'firstNameGroup' },
    { name: 'lastName', elementId: 'lastNameGroup' },
    { name: 'email', elementId: 'emailGroup' },
    { name: 'phoneNumber', elementId: 'phoneNumberGroup' },
  ];
  
  validEmail: boolean = false;
  validPhoneNumber: boolean = false;
  userRegistratiomSuccess : boolean = false;

  userGeneralForm: FormGroup;
  get firstName() { return this.userGeneralForm.get('firstName'); }
  get lastName() { return this.userGeneralForm.get('lastName'); }
  get email() { return this.userGeneralForm.get('email'); }
  get phoneNumber() { return this.userGeneralForm.get('phoneNumber'); }
  
  constructor(private authFacade: AuthFacadeService, private spinnerService: SpinnerService) {
    this.userGeneralForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.pattern(`^[A-ZА-ЯƏÜÖĞİŞÇ][A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç\s']{2,31}$`)]),
      lastName: new FormControl(null, [Validators.required, Validators.pattern(`^[A-ZА-ЯƏÜÖĞİŞÇ][A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç\s']{2,31}$`)]),
      email: new FormControl(null, [Validators.required, Validators.pattern('^[\\w\\-\\.]+@[\\w-]+\\.[a-z]{2,4}$')]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern('^\\+\\d{1,3}\\d{1,3}\\d{7}$')])
    });
  }
  
  async onSubmit() : Promise<void> {
    try {
      if(this.userGeneralForm.valid) {
        this.spinnerService.changeSpinnerState(true);
        await this.authFacade.userRegistration(this.userGeneralForm.value);
        this.spinnerService.changeSpinnerState(false);
        this.userRegistratiomSuccess = true;
      }
    } catch(error : any) {
      this.spinnerService.changeSpinnerState(false);
      console.log(error);
    }
  }

  async onInputBlur(fieldName: string) : Promise<void> {
    const field = this.inputFields.find(f => f.name === fieldName);
    if (field) {
      if(field.name === 'email') {
        if(this.userGeneralForm.get(fieldName)?.valid) {
          this.validEmail = await this.authFacade.checkEmailAvailability(this.email?.value);
          
          if(this.validEmail) {
            this.email?.setErrors({ 'emailNotAvailable': true });
            this.userGeneralForm.setErrors({ 'invalidData': true });
          }
        }
        
        this.changeValidity(field.name, field.elementId);

      } else if (field.name === 'phoneNumber') {
        if(fieldName === 'phoneNumber' && this.userGeneralForm.get(fieldName)?.valid) {
          this.validPhoneNumber = await this.authFacade.checkPhoneNumberAvailability(this.phoneNumber?.value);

          if(this.validPhoneNumber) {
            this.phoneNumber?.setErrors({ 'phoneNumberNotAvailable': true });
            this.userGeneralForm.setErrors({ 'invalidData': true });
          }
        }

        this.changeValidity(field.name, field.elementId);

      } else this.changeValidity(field.name, field.elementId);
    }
  }

  changeValidity(fieldName: string, elementId: string) : void {
    const element = $(`#${elementId}`);

    if (this.userGeneralForm.get(fieldName)?.valid) {
      element.removeClass('custom-invalid-card-group');
      element.addClass('custom-valid-card-group');
    } else {
      element.removeClass('custom-valid-card-group');
      element.addClass('custom-invalid-card-group');
    }
  }
}
