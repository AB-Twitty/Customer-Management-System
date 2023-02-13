export class ContactInfo {
    id!:number;
    customerId!:number;
    contact!:string;
    contactTypeId!:number;
    isActive!:boolean;
    isDeleted:boolean = false;
}