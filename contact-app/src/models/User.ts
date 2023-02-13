export class User {
    userId!:Number;
    name!:string;
    username!:string;
    email!:string;

    constructor(model:any) {
        this.userId = model?.userId,
        this.name = model?.name,
        this.username = model?.username,
        this.email = model?.email
    }
}