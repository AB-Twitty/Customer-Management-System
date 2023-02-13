import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AccountComponent } from './account/account.component';
import { EditUserComponent } from './account/edit-user/edit-user.component';
import { UserProfileComponent } from './account/user-profile/user-profile.component';
import { AddCustomerComponent } from './add-customer/add-customer.component';
import { AddContactComponent } from './customer-account/add-contact/add-contact.component';
import { AttachmentListComponent } from './customer-account/attachment-list/attachment-list.component';
import { ContactDetailsComponent } from './customer-account/contact-details/contact-details.component';
import { ContactListComponent } from './customer-account/contact-list/contact-list.component';
import { CustomerAccountComponent } from './customer-account/customer-account.component';
import { CustomerDetailsComponent } from './customer-account/customer-details/customer-details.component';
import { EditContactComponent } from './customer-account/edit-contact/edit-contact.component';
import { EditCustomerComponent } from './customer-account/edit-customer/edit-customer.component';
import { CustomersListComponent } from './customers-list/customers-list.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path:'login', component:LoginComponent},
  {path:'register', component:RegisterComponent},
  {path:'account', component:AccountComponent, children: [
    {path:'edit-profile', component:EditUserComponent},
    {path:'profile', component:UserProfileComponent}
  ]},
  {path:'customers/list/page/:pageIndex', component:CustomersListComponent},
  {path:'add-customer', component:AddCustomerComponent},
  {path:'customer-account/:customerId', component:CustomerAccountComponent, children:[
    {path:'details', component:CustomerDetailsComponent},
    {path:'contact-list', component:ContactListComponent},
    {path:'edit-contact/:contactId', component:EditContactComponent},
    {path:'contact-details/:contactId', component:ContactDetailsComponent},
    {path:'add-contact', component:AddContactComponent},
    {path:'attachment-list', component:AttachmentListComponent},
    {path:'edit', component:EditCustomerComponent},
  ]},
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
