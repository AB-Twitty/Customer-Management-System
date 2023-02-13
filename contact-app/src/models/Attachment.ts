export class Attachment {
    id!:number;
    attachmentURL!:string;
    isDeleted!:boolean;
    creator!:string;
    creationDate!:string;
    modifier!:string;
    lastModifiedDate!:string;

    constructor(model:any) {
        this.id = model.id;
        this.attachmentURL = model.attachmentUrl;
        this.isDeleted = model.isDeleted;
        this.creator = model.creator.username;
        this.creationDate = new Date(model.creationDate).toUTCString();
        this.modifier = model.modifier.username;
        this.lastModifiedDate = new Date(model.lastModifiedDate).toUTCString();
    }
}