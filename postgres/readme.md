# Postgres Docker Setup

Setup a local Postgres install with Docker

This guide based on https://info.crunchydata.com/blog/easy-postgresql-10-and-pgadmin-4-setup-with-docker

## Set Config Values 
Edit the pg-env and pgadmin-env files.

Replace the <config_value> value with your information.

## Run the Install Script

`.\install-postgres`

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


## Managing
To stop the containers

`docker stop postgres pgadmin4`

To start the containers

`docker start postgres pgadmin4`

To remove the containers, first stop them then

`docker rm postgres pgadmin4`




