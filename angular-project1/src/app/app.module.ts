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
// import { NewProjectComponent } from './new-project/new-project.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import { ProjectComponent } from './project/project.component';
import {MultiSelectModule} from 'primeng/multiselect';
import { PasswordDirective } from './password.directive';
import { FilterComponent } from './filter/filter.component';
// import {FilterService} from 'primeng/api';
import {InputNumberModule} from 'primeng/inputnumber';
import {TableModule} from 'primeng/table';
import {InputSwitchModule} from 'primeng/inputswitch';
import {DialogModule} from 'primeng/dialog';
import { SendEmailComponent } from './send-email/send-email.component';
import {EditorModule} from 'primeng/editor';
import { HomeComponent } from './home/home.component';


 // import {HttpClientModule{ from '@angular/common/http'};
export const ROUTES: Routes = [
   { path: '', component:LoginComponent },
   { path: 'New-Company', component: NewCompanyComponent } ,
   { path: 'project', component: ProjectComponent },
   { path: 'filter', component: FilterComponent },
   { path: 'sendEmail', component: SendEmailComponent },
   { path: 'home', component: HomeComponent }
];
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NewCompanyComponent,
    InputDirective,
    MaxLenghDirective,
    ProjectComponent,
    PasswordDirective,
    FilterComponent,
    SendEmailComponent,
    HomeComponent,
    // NewProjectComponent
   
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
    MultiSelectModule,
    MessagesModule,
    MessageModule,
    AutoCompleteModule,
    InputNumberModule,
    TableModule,
    InputSwitchModule,
    DialogModule,
    EditorModule,
    
    
  ],
  providers: [],
  bootstrap: [AppComponent]

})
export class AppModule { }
 

