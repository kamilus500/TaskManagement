import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { Observable } from 'rxjs';
import { TaskjobService } from '../../services/taskjob.service';
import { TaskJob } from '../../models/TaskJob';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {

  taskJobs$: Observable<TaskJob[]> = this.taskjobService.taskJobs$;
  userId: string | null = null;

  constructor(private accountService: AccountService, private taskjobService: TaskjobService) {
    this.accountService.userId$
      .subscribe(userId => this.userId = userId );
  }

  ngOnInit(): void {
    if (this.userId) {
      this.taskjobService.loadTaskJobs(this.userId)
    }
  }
}
