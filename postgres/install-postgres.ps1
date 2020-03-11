# create the postgres volume
docker volume create --driver local --name=pgvolume

#setup a bridge network
docker network create --driver bridge pgnetwork

#install postgres
docker run --publish 5432:543 --volume=pgvolume:/pgdata --env-file=pg-env.list --name=postgres --hostname=postgres --network=pgnetwork --detach crunchydata/crunchy-postgres:centos7-10.9-2.4.1

# create the pgadmin volumn
docker volume create --driver local --name=pga4volume

#install pgadmin
docker run --publish 5050:5050 --volume=pga4volume:/var/lib/pgadmin --env-file=pgadmin-env.list --name=pgadmin4 --hostname=pgadmin4 --network=pgnetwork --detach crunchydata/crunchy-pgadmin4:centos7-10.9-2.4.1