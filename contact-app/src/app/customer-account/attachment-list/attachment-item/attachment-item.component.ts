import { Component, Input, OnInit } from '@angular/core';
import { Attachment } from 'src/models/Attachment';
import { AttachmentService } from 'src/services/attachment.service';

@Component({
  selector: 'app-attachment-item',
  templateUrl: './attachment-item.component.html',
  styleUrls: ['./attachment-item.component.css']
})
export class AttachmentItemComponent implements OnInit {

  @Input() attachment!:Attachment;
  constructor(private attachmentService:AttachmentService) { }

  ngOnInit(): void {
  }

  onDelete(id:number) {
    this.attachment = new Attachment(this.attachmentService.deleteAttachment(id).subscribe());
  }

  onRestore(id:number) {
    this.attachment = new Attachment(this.attachmentService.restoreAttachment(id).subscribe());
  }

}
