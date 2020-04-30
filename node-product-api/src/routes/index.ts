import * as express from "express";
import * as products from "../products/product.route";
import * as brands from '../brands/brand.route';

export const register = (app: express.Application) => {
    const oidc = app.locals.oidc;

    // register the product routes
    products.register(app);
    brands.register(app);
};
