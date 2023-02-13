import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Attachment } from 'src/models/Attachment';

@Injectable({
  providedIn: 'root'
})
export class AttachmentService {

  attachments:Attachment[] = [];
  attachment!:Attachment;

  baseUrl:string = 'https://localhost:44321/api/Attachment/'
  constructor(private http:HttpClient) { }

  getAttachmentsFor(customerId:number) {
    return this.http.get(this.baseUrl+'GetAttachmentsFor/'+customerId).pipe(
      map((response:any) => {
        this.attachments = [];
        if(response.dataList) 
          response.dataList.forEach((element:any) => {
            this.attachments.push(new Attachment(element));
          });
        return this.attachments;
      })
    )
  }

  addAttachmentTo(file:File,customerId:number) {
    let testData:FormData = new FormData();
    testData.append('attachmentFile', file, file.name);
    console.log(testData);
    return this.http.post(this.baseUrl+'AddAttachmentTo/'+customerId, testData);
  }

  deleteAttachment(id:number) {
    return this.http.get(this.baseUrl+'DeleteAttachment/'+id).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.attachments.forEach(element => {
            if(element.id===response.data.id) {
              element.isDeleted = response.data.isDeleted;
            }
          });
        }
      })
    )
  }

  restoreAttachment(id:number) {
    return this.http.get(this.baseUrl+'RestoreAttachment/'+id).pipe(
      map((response:any) => {
        if(response.status==200) {
          this.attachments.forEach(element => {
            if(element.id===response.data.id) {
              element.isDeleted = response.data.isDeleted;
            }
          });
        }
      })
    )
  }
}
