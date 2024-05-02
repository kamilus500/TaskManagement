import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class JwtdecryptionService {
  private readonly AES_KEY = 'secretKey';

  encrypt(value: string): string {
      return CryptoJS.AES.encrypt(value, this.AES_KEY).toString();
  }

  decrypt(value: string): string {
      return (CryptoJS.AES.decrypt(value, this.AES_KEY)).toString(CryptoJS.enc.Utf8);
  }
}
