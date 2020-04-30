import dotenv from 'dotenv'
dotenv.config();

export const SERVER_PORT = process.env.SERVER_PORT;
export const POSTGRES_HOST = process.env.POSTGRES_HOST;
export const POSTGRES_PORT = process.env.POSTGRES_PORT;
export const POSTGRES_USER = process.env.POSTGRES_USER;
export const POSTGRES_PASS = process.env.POSTGRES_PASS;