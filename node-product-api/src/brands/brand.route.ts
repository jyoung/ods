import * as express from "express";
import logger from '../config/logger';
import {db} from '../db';
import { CreateCommand } from "./commands/create.command";

export const register = (app: express.Application) => {
    // simple sanity check
    app.get(`/api/brands/sanity-check`, async (req: express.Request, res: express.Response) => {
        res.json({ message: "Ok" });
    });

    app.get(`/api/brands`, async (req: express.Request, res: express.Response) => {
        try {
            const brands = await db.brands.all();
            return res.json(brands);

        } catch (err) {
            logger.error(err);
            res.json({ error: err.message || err });
        }
    });

    app.post(`/api/brands`, async (req: express.Request, res: express.Response) => {

        
        res.statusCode = 201;
        res.json(1);
    });
}