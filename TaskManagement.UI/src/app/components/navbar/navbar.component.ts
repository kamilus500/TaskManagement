import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { TokenService } from '../../services/token.service';
import { Router } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { DialogService } from 'primeng/dynamicdialog';
import { ModalEnum } from '../../enums/ModalEnum';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  loginStatus$: Observable<boolean> = this.accountService.status$;
  login$: Observable<string | null> = this.accountService.login$;

  constructor(
    private accountService: AccountService, 
    private tokenService: TokenService, 
    private router: Router,
    private dialogService: DialogService
    ) {
    
  }

  openModal(modal: string) {
    if (modal === ModalEnum.Login) {
      this.dialogService.open(LoginComponent, {
        header: 'Login',
        width: '25vw',
        modal: true
      })
    } else {
      this.dialogService.open(RegisterComponent, {
        header: 'Register',
        width: '25vw',
        modal: true
      })
    }
  }

  logout(): void {
    this.tokenService.removeToken();

    this.accountService.setLoginStatus(false);
    this.accountService.setLogin(null);
    
    this.router.navigate(['/']);
  }
}
