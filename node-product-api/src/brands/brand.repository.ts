import {IDatabase, IMain} from 'pg-promise';
// import {IResult} from 'pg-promise/typescript/pg-subset';
import { Brand } from "./brand.model";


export class BrandRepository {

    /**
     * @param db
     * Automated database connection context/interface.
     *
     * If you ever need to access other repositories from this one,
     * you will have to replace type 'IDatabase<any>' with 'any'.
     *
     * @param pgp
     * Library's root, if ever needed, like to access 'helpers'
     * or other namespaces available from the root.
     */
    constructor(private db: IDatabase<any>, private pgp: IMain) {
        /*
          If your repository needs to use helpers like ColumnSet,
          you should create it conditionally, inside the constructor,
          i.e. only once, as a singleton.
        */
    }

    // Returns all brands records;
    async all(): Promise<Brand[]> {
        return this.db.any('SELECT * FROM brands');
    }

    // Returns the total number of brands;
    async total(): Promise<number> {
        return this.db.one('SELECT count(*) FROM brands', [], (a: { count: string }) => +a.count);
    }
}