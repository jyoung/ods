import * as express from "express";
import * as products from "./products";

export const register = (app: express.Application) => {
    const oidc = app.locals.oidc;

    // register the product routes
    products.register(app);
};
