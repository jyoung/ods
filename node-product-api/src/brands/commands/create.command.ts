import { db } from "../../db";

export class CreateCommand {
    public name: string = "";

    public async Execute(): Promise<number> {

       return 1;
    }
}