import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AttachmentService } from 'src/services/attachment.service';

@Component({
  selector: 'app-attachment-list',
  templateUrl: './attachment-list.component.html',
  styleUrls: ['./attachment-list.component.css']
})
export class AttachmentListComponent implements OnInit {

  attachmentFile!:File;
  attachments:any;
  constructor(public attachmentService:AttachmentService, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.attachmentService.getAttachmentsFor(+this.route.parent?.snapshot.params['customerId']).subscribe(
      response => this.attachments=response
    );
  }
  
  inputChange(event:any) {
    if (event)
      this.attachmentFile = event.target.files[0];
  }

  onAddAttachment() {
    this.attachmentService.addAttachmentTo(this.attachmentFile,+this.route.parent?.snapshot.params['customerId']).subscribe();
  }

}
