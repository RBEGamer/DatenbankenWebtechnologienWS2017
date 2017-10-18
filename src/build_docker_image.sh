#/!bin/bash
dotnet publish -c Release
docker build -t dbwt .
docker run -d --name dbwt dbwt
