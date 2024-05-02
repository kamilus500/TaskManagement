import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  loginStatus$: Observable<boolean> = this.accountService.status$;

  constructor(private accountService: AccountService) {

  }
}
