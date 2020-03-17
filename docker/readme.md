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

## Migrations

The OutdoorShop.Catalog.Migrations takes care of creating the all the schemas, tables and permissions. 

The configuration is managed by dotnet-usersecrets so you will need to add a user secrtes for postgres-admin and postgres-user.

Run the migrations project to create the data structures.

## Seed Data

The OutdoorShop.Catalog.Seed seeds the database with test data.

After the migrations have been run, run the seed project. It uses the same secrets as the migrations project.

## Managing
To stop the containers

`docker-compose stop`

To start the containers

`docker-compose start`

To remove the containers

`docker-compose down`




