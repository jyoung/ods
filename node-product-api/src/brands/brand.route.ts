import * as express from "express";
import {db} from '../db';

export const register = (app: express.Application) => {
    // simple sanity check
    app.get(`/api/brands/sanity-check`, async (req: express.Request, res: express.Response) => {
        res.json({ message: "Ok" });
    });

    app.get(`/api/brands`, async (req: any, res: express.Response) => {
        try {
            const brands = await db.brands.all();
            return res.json(brands);

        } catch (err) {
            // tslint:disable-next-line:no-console
            console.error(err);
            res.json({ error: err.message || err });
        }
    });
}