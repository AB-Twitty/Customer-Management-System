import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactService } from 'src/services/contact.service';
import { TypeService } from 'src/services/type.service';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrls: ['./add-contact.component.css']
})
export class AddContactComponent implements OnInit {

  addContactForm!:FormGroup;
  customerId!:number;
  types:any[] = [];
  constructor(private contactService:ContactService, private typeService:TypeService, 
      private route:ActivatedRoute, private router:Router) {
    this.typeService.getContactTypes().subscribe(response => this.types=response);
    this.customerId = route.parent?.snapshot.params['customerId'];
   }

  ngOnInit(): void {
    this.addContactForm = new FormGroup({
      'customerId': new FormControl(this.customerId, Validators.required),
      'contactTypeId': new FormControl(0, Validators.required),
      'contact': new FormControl({value:null, disabled:true}, Validators.required),
      'isActive': new FormControl(true, Validators.required),
    })
  }

  onAddContact() {
    console.log(this.addContactForm);
    if(!this.addContactForm.get('contact')?.disabled) {
      this.contactService.addContact(this.addContactForm.value).subscribe((response:any) => {
        if(response.status==200) {
          this.router.navigate(["../contact-list"], {relativeTo:this.route});
        }
        else if (response.status==422) {
          response.errorMessages.forEach((element:any) => {
            this.addContactForm.get(element.prop)?.setErrors({"exist": element.errors[0]});
          });
        }
      });
    }
  }

  onTypeChange(selectedType:String) {
    this.addContactForm.get('contact')?.reset();
    for(let type of this.types) 
      if(type.id==selectedType) {
        const validationExp = type.validationExpression;
        this.addContactForm.get('contact')?.enable();
        this.addContactForm.get('contact')?.addValidators(Validators.pattern(validationExp));
        break;
      }
  }

}
