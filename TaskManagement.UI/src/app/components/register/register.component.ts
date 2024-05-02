import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { RegisterDto } from '../../models/RegisterDto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private accountService: AccountService, private fb: FormBuilder,private ref: DynamicDialogRef) {
    this.registerForm = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]]
    })
  }

  onSubmit() {
    const registerDto = this.registerForm.value;
    
    if (registerDto !== undefined || registerDto !== null) {
      this.accountService.register(registerDto)
        .subscribe((response: string) => {
          if (response) {
            this.ref.close()
          }
        })
    }
  }
}
