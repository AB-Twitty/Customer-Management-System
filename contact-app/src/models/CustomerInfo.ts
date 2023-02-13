export class CustomerInfo {
    id!:number;
    firstName!:string;
    lastName!:string;
    birthDate!:string;
    nationalId!:string;
    nationality!:string;
    nationalityId!:number;
    imageURL!:string;
    imageFile!:File;
    isActive!:boolean;
    isDeleted!:boolean;

    constructor(model:any=null, nationality:string="") {
        this.id = +model?.id;
        this.firstName = model?.firstName;
        this.lastName = model?.lastName;
        this.imageURL = model?.customerImageURL!=null?model?.customerImageURL:"default.png";
        this.birthDate = new Date(model?.birthDate).toDateString();
        this.nationalId = model?.nationalId;
        this.isActive = model?.isActive;
        this.isDeleted = model?.isDeleted;
        this.nationalityId = model?.nationality?.id;
        this.nationality = nationality;
    }

}