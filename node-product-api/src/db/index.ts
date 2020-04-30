import * as promise from 'bluebird'; // best promise library today
import pgPromise from 'pg-promise'; // pg-promise core library
import {Diagnostics} from './diagnostics'; // optional diagnostics
import { POSTGRES_HOST, POSTGRES_PORT, POSTGRES_USER, POSTGRES_PASS } from "../config/env";
import {IInitOptions, IDatabase, IMain} from 'pg-promise';
import {IExtensions, BrandRepository} from './extenstions';

type ExtendedProtocol = IDatabase<IExtensions> & IExtensions;

// should this be called multiple times?
// seems like it would be better to init the variables once, as in the main index file.
// dotenv.config();

// pg-promise initialization options:
const initOptions: IInitOptions<IExtensions> = {

    // Using a custom promise library, instead of the default ES6 Promise:
    promiseLib: promise,

    // Extending the database protocol with our custom repositories;
    // API: http://vitaly-t.github.io/pg-promise/global.html#event:extend
    extend(obj: ExtendedProtocol, dc: any) {
        // Database Context (dc) is mainly needed for extending multiple databases with different access API.

        // Do not use 'require()' here, because this event occurs for every task and transaction being executed,
        // which should be as fast as possible.
        // obj.users = new UsersRepository(obj, pgp);
        // obj.products = new ProductsRepository(obj, pgp);
        obj.brands = new BrandRepository(obj, pgp);
    }
};

const postgresHost = POSTGRES_HOST || "";
const postgresPort = POSTGRES_PORT || 0;
const postgresUser = POSTGRES_USER || "";
const postgresPass = POSTGRES_PASS || "";

const dbConfig = {
    "host": postgresHost,
    "port": +postgresPort,
    "database": "ods",
    "user": postgresUser,
    "password": postgresPass
}

// Initializing the library:
const pgp: IMain = pgPromise(initOptions);

// Creating the database instance with extensions:
const db: ExtendedProtocol = pgp(dbConfig);

// Initializing optional diagnostics:
Diagnostics.init(initOptions);

// Alternatively, you can get access to pgp via db.$config.pgp
// See: https://vitaly-t.github.io/pg-promise/Database.html#$config
export {db, pgp};