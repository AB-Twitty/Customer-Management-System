import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountComponent } from './account/account.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavComponent } from './nav/nav.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SideBarComponent } from './account/side-bar/side-bar.component'
import { EditUserComponent } from './account/edit-user/edit-user.component';
import { UserProfileComponent } from './account/user-profile/user-profile.component';
import { CustomersListComponent } from './customers-list/customers-list.component';
import { CustomerItemComponent } from './customers-list/customer-item/customer-item.component';
import { CustomerAccountComponent } from './customer-account/customer-account.component';
import { CustomerBarComponent } from './customer-account/customer-bar/customer-bar.component';
import { CustomerDetailsComponent } from './customer-account/customer-details/customer-details.component';
import { ContactListComponent } from './customer-account/contact-list/contact-list.component';
import { ContactItemComponent } from './customer-account/contact-list/contact-item/contact-item.component';
import { ContactDetailsComponent } from './customer-account/contact-details/contact-details.component';
import { AttachmentListComponent } from './customer-account/attachment-list/attachment-list.component';
import { AttachmentItemComponent } from './customer-account/attachment-list/attachment-item/attachment-item.component';
import { EditCustomerComponent } from './customer-account/edit-customer/edit-customer.component';
import { AddContactComponent } from './customer-account/add-contact/add-contact.component';
import { AddCustomerComponent } from './add-customer/add-customer.component';
import { EditContactComponent } from './customer-account/edit-contact/edit-contact.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountComponent,
    NavComponent,
    RegisterComponent,
    LoginComponent,
    SideBarComponent,
    EditUserComponent,
    UserProfileComponent,
    CustomersListComponent,
    CustomerItemComponent,
    CustomerAccountComponent,
    CustomerBarComponent,
    CustomerDetailsComponent,
    ContactListComponent,
    ContactItemComponent,
    ContactDetailsComponent,
    AttachmentListComponent,
    AttachmentItemComponent,
    EditCustomerComponent,
    AddContactComponent,
    AddCustomerComponent,
    EditContactComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    AppRoutingModule,
    ReactiveFormsModule,
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
