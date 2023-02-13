import { CustomerInfo } from "./CustomerInfo";

export class CustomerDetail extends CustomerInfo {
    creationDate!:string;
    creatorName!:string;
    lastModifiedDate!:string;
    modifierName!:string;

    constructor(model:any = null, nationality:string) {
        super(model,nationality);
        this.creationDate = new Date(model?.creationDate).toUTCString();
        this.creatorName = model?.creator.username;
        this.lastModifiedDate = new Date(model?.lastModifiedDate).toUTCString();
        this.modifierName = model?.modifier.username;
    }

}