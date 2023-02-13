import { User } from "./User";

export class Registration extends User {
    password!:string;
    confirmPassword!:string;

    constructor(model:any = null) {
        super(model);
        this.password = model?.password;
        this.confirmPassword = model?.confirmPassword;
    }
}