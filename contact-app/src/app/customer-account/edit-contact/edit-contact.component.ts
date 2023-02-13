import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Contact } from 'src/models/Contact';
import { ContactType } from 'src/models/ContactType';
import { ContactService } from 'src/services/contact.service';
import { TypeService } from 'src/services/type.service';

@Component({
  selector: 'app-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.css']
})
export class EditContactComponent implements OnInit {

  editContactForm!:FormGroup;
  types:ContactType[] = [];
  contactId!:number;
  customerId!:number;
  constructor(private contactService:ContactService, private typeService:TypeService, 
      private route:ActivatedRoute, private router:Router) {
    this.editContactForm = new FormGroup({
      'isActive': new FormControl(true),
      'contactTypeId': new FormControl(0),
      'contact': new FormControl(),
    })
    this.contactId = +route.snapshot.params['contactId'];
    this.customerId = route.parent?.snapshot.params['customerId'];
    this.typeService.getContactTypes().subscribe(response => this.types=response);
    
   }

  ngOnInit(): void {
    this.contactService.getContactById(this.contactId).subscribe(response => {
      this.editContactForm = new FormGroup({
        'id': new FormControl(this.contactId, Validators.required),
        'customerId': new FormControl(this.customerId, Validators.required),
        'contactTypeId': new FormControl(
          {value:this.contactService.contact.contactType.id, disabled:true} ,
          Validators.required
          ),
        'contact': new FormControl(
          this.contactService.contact.contact, 
          [Validators.required,  Validators.pattern(this.contactService.contact.contactType.validationExpression)]
          ),
        'isActive': new FormControl(this.contactService.contact.isActive, Validators.required),
      });
    });
  }

  onEditContact() {
    console.log(this.editContactForm);
    if(this.editContactForm.valid){
      this.contactService.editContact(this.editContactForm.value).subscribe((response:any) => {
        if(response.status==200) {
          this.router.navigate(['../../contact-list'], {relativeTo:this.route});
        } else if (response.status==422) {
                    response.errorMessages.forEach((element:any) => {
            this.editContactForm.get(element.prop)?.setErrors({"exist": element.errors[0]});
          });
        }
      });
    }
  }

}
