import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { RegisterDto } from '../models/RegisterDto';
import { LoginResponse } from '../models/LoginResponse';
import { LoginDto } from '../models/LoginDto';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private apiUrl = 'https://localhost:7202';

  status$:BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false); 
  login$: BehaviorSubject<string|null> = new BehaviorSubject<string|null>(null);
  userId$: BehaviorSubject<string|null> = new BehaviorSubject<string|null>(null);

  constructor(private httpClient: HttpClient, private tokenService: TokenService) {

  }

  setLoginStatus(status: boolean): void {
    this.status$.next(status);
  }

  setLogin(login: string|null): void {
      this.login$.next(login);
  }

  setUserId(userId: string|null): void {
      this.userId$.next(userId);
  }

  register(registerDto: RegisterDto): Observable<string> {
    return this.httpClient.post<string>(this.apiUrl + '/register', registerDto);
  }

  login(loginDto: LoginDto): Observable<LoginResponse> {
    return this.httpClient.post<LoginResponse>(this.apiUrl + '/login', loginDto);
  }
}
