import { Injectable } from '@angular/core';
import { TaskJob } from '../models/TaskJob';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Task } from 'zone.js/lib/zone-impl';

@Injectable({
  providedIn: 'root'
})
export class TaskjobService {
  private apiUrl = 'https://localhost:7202';

  taskJobs$: BehaviorSubject<TaskJob[]> = new BehaviorSubject<TaskJob[]>([]);
  taskJob$: BehaviorSubject<TaskJob|null> = new BehaviorSubject<TaskJob|null>(null);
  constructor(private httpClient: HttpClient) { 

  }

  loadTaskJobs(userId: string): void {
    this.getTaskJobs(userId)
      .subscribe(taskJobs => this.taskJobs$.next(taskJobs));
  }

  loadTaskJob(taskJobId: string): void {
    this.getTaskById(taskJobId)
      .subscribe(taskJob => this.taskJob$.next(taskJob));
  }

  getTaskJobs(userId: string): Observable<TaskJob[]> {
    return this.httpClient.get<TaskJob[]>(this.apiUrl + '/get/' + userId);
  }

  getTaskById(taskjobId: string): Observable<TaskJob> {
    return this.httpClient.get<TaskJob>(this.apiUrl + '/getbyid/' + taskjobId);
  }

  createTaskJob(taskjob: Task): Observable<string> {
    return this.httpClient.post<string>(this.apiUrl + '/create/', taskjob);
  }

  removeTaskJob(taskId: string): Observable<void> {
    return this.httpClient.delete<void>(this.apiUrl + '/remove/' + taskId);
  }

  updateTaskJob(taskJob: TaskJob): Observable<void> {
    return this.httpClient.put<void>(this.apiUrl + '/update/', taskJob);
  }
}
