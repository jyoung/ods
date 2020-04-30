# Postgres Docker Setup

Setup a local Postgres install with Docker

This was guide was orignally based on https://info.crunchydata.com/blog/easy-postgresql-10-and-pgadmin-4-setup-with-docker

It now works with docker compose to spin up postgres, pgadmin and redis.

## Set Config Values 
Edit the pg-env and pgadmin-env files.

Replace the <config_value> value with your information.

## Run the Install Script

`docker-compose up`

## Configure pgAdmin

1. Browse to http://localhost:5050
2. Log in with the username and password defined in pgadmin-env.
3. Select 'Servers' on the left
4. Select 'Object' -> 'Create' -> 'Server' on the top menu 
5. Enter 'PG10' for the Name
6. Select the 'Connection' tab
7. Enter 'postgres' for the Host
8. Enter your username and password from pg-env
9. Select 'Save'

## Connect to Postgres

Run `docker-compose run postgres bash` to get to the postgres bash.

Execute `psql -h postgres -d ods -U postgres` to connect to to the ods database as the postgres super user.

Execute `grant usage on schema ods to [your_username];` to get access to the ods schema in the ods database.

Execute `grant all privileges on all tables in schema ods to [your_username];` to get access to all the tables in ods.

Type `\q` to quit the ods connection.

Type `ctrl+d` to quit the postgres bash.

## User Secrets

The dotnet projects are configured using the user-secrets dotnet tool.

All the dotnet projects use the same secrets config, so you can add the secrets in the Api, Migrations, or Seed projects.

Replace the values in < > with the values in your pg-env.list.

`dotnet user-secrets set "postgres-user" "Server=127.0.0.1;Port=5432;Database=ods;User Id=<PG_USER>;Password=<PG_PASSWORD>;"`

`dotnet user-secrets set "postgres-admin" "Server=127.0.0.1;Port=5432;Database=ods;User Id=postgres;Password=<PG_ROOT_PASSWORD>;"`

`dotnet user-secrets set "redis-url" "localhost:6379"`

## Migrations

The OutdoorShop.Catalog.Migrations takes care of creating the all the schemas, tables and permissions. 

Run the migrations project to create the data structures.

## Seed Data

The OutdoorShop.Catalog.Seed seeds the database with test data.

After the migrations have been run, execute the seed project. 

## Managing
To stop the containers

`docker-compose stop`

To start the containers

`docker-compose start`

To remove the containers

`docker-compose down`




