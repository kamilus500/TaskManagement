import { Component } from '@angular/core';
import { TokenService } from './services/token.service';
import { AccountService } from './services/account.service';
import { PropertyEnum } from './components/enums/PropertyEnum';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'TaskManagement.UI';

  constructor(private tokenService: TokenService, private accountService: AccountService) {

  }

  ngOnInit(): void {
    let login = this.tokenService.getUserProperty(PropertyEnum.Login);
    let userId = this.tokenService.getUserProperty(PropertyEnum.UserId);

    this.accountService.setLogin(login);
    this.accountService.setUserId(userId);

    this.accountService.setLoginStatus(userId ? true : false);
  }
}
