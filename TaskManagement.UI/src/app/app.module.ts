import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ButtonModule } from 'primeng/button';
import { NavbarComponent } from './components/navbar/navbar.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AccountService } from './services/account.service';
import { TokenService } from './services/token.service';
import { JwtdecryptionService } from './services/jwtdecryption.service';
import { TokenInterceptor } from './tokenInterceptor';
import { DialogModule } from 'primeng/dialog';
import { DialogService } from 'primeng/dynamicdialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { TaskjobService } from './services/taskjob.service';
import { DragDropModule } from 'primeng/dragdrop';
import { TableModule } from 'primeng/table';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    LoginComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    DialogModule,
    BrowserAnimationsModule,
    InputTextModule,
    PasswordModule,
    DragDropModule,
    TableModule
  ],
  providers: [
    JwtdecryptionService,
    TokenService,
    AccountService,
    TaskjobService,
    { 
      provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true 
    },
    DialogService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
