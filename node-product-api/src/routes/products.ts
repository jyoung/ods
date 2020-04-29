import * as express from "express";

export const register = (app: express.Application) => {
    // simple sanity check
    app.get(`/api/products/sanity-check`, async (req: express.Request, res: express.Response) => {
        res.json({ message: "Ok" });
    });
}