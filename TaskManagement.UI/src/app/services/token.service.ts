import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { PropertyEnum } from '../components/enums/PropertyEnum';
import { JwtdecryptionService } from './jwtdecryption.service';
import { CustomJwtPayload } from '../models/CustomJwtPayload';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private readonly TOKEN_KEY = 'token';

  constructor(private jwtDecodeService: JwtdecryptionService) {
    
  }

  setToken(token: string): void {
    const encryptedValue = this.jwtDecodeService.encrypt(token);
    localStorage.setItem(this.TOKEN_KEY, encryptedValue);
  }

  getToken(): string | null {
    const encryptedValue = localStorage.getItem(this.TOKEN_KEY);

    if(encryptedValue) {
      return this.jwtDecodeService.decrypt(encryptedValue);
    }

    return null;
  }

  getUserProperty(property: PropertyEnum): string | null {
    const encryptedValue = localStorage.getItem(this.TOKEN_KEY);

    if(encryptedValue) {
      let decryptedValue = this.jwtDecodeService.decrypt(encryptedValue);

      let decoded = jwtDecode<CustomJwtPayload>(decryptedValue);
      
      switch(property) {
        case PropertyEnum.Login:
          return decoded.Login;
        case PropertyEnum.UserId:
          return decoded.UserId;
      }
    }

    return null;
  }

  removeToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }
}
