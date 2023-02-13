import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Contact } from 'src/models/Contact';
import { ContactService } from 'src/services/contact.service';

@Component({
  selector: 'app-contact-item',
  templateUrl: './contact-item.component.html',
  styleUrls: ['./contact-item.component.css']
})
export class ContactItemComponent implements OnInit {

  @Input() contact!:Contact;
  
  constructor(private contactService:ContactService, private router:Router, private route:ActivatedRoute) { }

  ngOnInit(): void {
  }

  onDelete(contactId:number) {
    this.contactService.deleteContact(contactId).subscribe();
  }

  onRestore(contactId:number) {
    this.contactService.restoreContact(contactId).subscribe();
  }

}
