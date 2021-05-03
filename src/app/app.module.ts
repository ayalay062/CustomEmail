import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
// import { RouterModule, ROUTES} from '@angular/router';
import { InputDirective } from './directievs/input.directive';
import { MaxLenghDirective } from './directievs/max-lengh.directive';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { TryComponent } from './try/try.component';
import { LoginComponent } from './login/login.component';
import {ButtonModule} from 'primeng/button';
import { NewCompanyComponent } from './new-company/new-company.component';
import {DropdownModule} from 'primeng/dropdown';
import {AccordionModule} from 'primeng/accordion';     //accordion and accordion tab
import {InputTextModule} from 'primeng/inputtext';
import {FileUploadModule} from 'primeng/fileupload';
import { Router } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import {CalendarModule} from 'primeng/calendar';
import {PasswordModule} from 'primeng/password';
import {SelectButtonModule} from 'primeng/selectbutton';
import {MessagesModule} from 'primeng/messages';
import {MessageModule} from 'primeng/message';
import { ProjectComponent } from './project/project.component';
// import {ProjectComponent } from './project/project.component';
// import {HttpClientModule{ from '@angular/common/http'};
export const ROUTES: Routes = [
   { path: 'login', component:LoginComponent },
   { path: 'New-Company', component: NewCompanyComponent } ,
   { path: 'project', component: ProjectComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NewCompanyComponent,
    InputDirective,
    MaxLenghDirective,
    ProjectComponent,
    // ProjectComponent
   
    // MaxLenthDirective,
    // TruePasswordDirective,
  
  ],
  
imports: [
    BrowserModule,
    FormsModule,
    // RouterModule.forRoot(ROUTES,{enableTracing:true}),
    RouterModule.forRoot(ROUTES),
    // Router,
    InputTextModule,
    AccordionModule,
    HttpClientModule,
    MatSliderModule,
    BrowserAnimationsModule,
    ButtonModule,
    DropdownModule,
    FileUploadModule,
    ReactiveFormsModule,
    CalendarModule,
    PasswordModule,
    SelectButtonModule,
    MessagesModule,
    MessageModule,
    DropdownModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]

})
export class AppModule { }
 

