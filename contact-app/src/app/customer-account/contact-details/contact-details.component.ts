import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Contact } from 'src/models/Contact';
import { ContactService } from 'src/services/contact.service';

@Component({
  selector: 'app-contact-details',
  templateUrl: './contact-details.component.html',
  styleUrls: ['./contact-details.component.css']
})
export class ContactDetailsComponent implements OnInit {

  constructor(public contactService:ContactService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.contactService.getContactById(this.route.snapshot.params['contactId']).subscribe();
  }

}
