import { ContactType } from "./ContactType";

export class Contact {
    id!:number;
    customerId!:number;
    contact!:string;
    isActive!:boolean;
    isDeleted!:boolean;
    creator!:string;
    creationDate!:string;
    modifier!:string;
    lastModifiedDate!:string;
    contactType!:ContactType;

    constructor(model:any = null) {
        this.id = model?.id;
        this.customerId = model?.customerId;
        this.contact = model?.contact;
        this.isActive = model?.isActive;
        this.isDeleted = model?.isDeleted;
        this.creator = model?.creator?.username;
        this.creationDate = new Date(model?.creationDate).toUTCString();
        this.modifier = model?.modifier?.username;
        this.lastModifiedDate = new Date(model?.lastModifiedDate).toUTCString();
        this.contactType = new ContactType(model?.contactType);
    }
}