import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ContactService } from 'src/services/contact.service';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {

  constructor(public contactService:ContactService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.contactService.getContactsFor(+this.route.parent?.snapshot.params['customerId']).subscribe();
  }

}
