docker pull microsoft/mssql-server-linux:2017-CU13

docker container kill mssql_lexicon
docker rm mssql_lexicon

docker run --detach --name=mssql_lexicon -p 1433:1433  -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123" microsoft/mssql-server-linux:2017-CU13
docker start mssql_lexicon

docker ps --all