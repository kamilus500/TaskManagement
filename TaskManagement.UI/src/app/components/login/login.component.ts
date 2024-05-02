import { Component } from '@angular/core';
import { LoginDto } from '../../models/LoginDto';
import { AccountService } from '../../services/account.service';
import { LoginResponse } from '../../models/LoginResponse';
import { TokenService } from '../../services/token.service';
import { Router } from '@angular/router';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PropertyEnum } from '../../enums/PropertyEnum';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private accountService: AccountService,
    private tokenService: TokenService,
    private router: Router,
    private ref: DynamicDialogRef,
    private fb: FormBuilder
  ) {
    this.loginForm = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onSubmit(): void {
    let loginDto: LoginDto = this.loginForm.value;

    this.accountService.login(loginDto)
        .subscribe((response: LoginResponse) => {
          if (response.token) {
            this.tokenService.setToken(response.token);
            
            let fullName = this.tokenService.getUserProperty(PropertyEnum.Login);
            let userId = this.tokenService.getUserProperty(PropertyEnum.UserId);

            this.accountService.setLogin(fullName);
            this.accountService.setUserId(userId);
            this.accountService.setLoginStatus(true);
          }

          this.ref.close();
        })
  }
}
