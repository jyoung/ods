import express from "express";
import { SERVER_PORT } from "./config/env";
import logger  from "./config/logger";
import * as routes from "./routes/index";

// port is now available to the Node.js runtime
// as if it were an environment variable
const port = SERVER_PORT;

const app = express();

// define a route handler for the default home page
app.get( "/", ( req, res ) => {
    res.send( "Hello world!" );
} );

// configure routes
routes.register(app);

// start the Express server
app.listen( port, () => {
    // tslint:disable-next-line:no-console
    // console.log( `server started at http://localhost:${ port }` );
    logger.info(`server started at http://localhost:${ port }`);
} );