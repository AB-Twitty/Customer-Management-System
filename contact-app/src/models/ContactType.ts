export class ContactType {
    id!:number;
    typeName!:string;
    validationExpression!:string;

    constructor(model:any = null) {
        this.id = model?.id;
        this.typeName = model?.typeName;
        this.validationExpression = model?.validationExpression;
    }
}