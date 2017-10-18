#!/bin/bash
cd dbwt
echo "get aws login by using bash eval"
eval $( aws ecr get-login --no-include-email --region eu-west-1)

echo "build netcore app"
dotnet publish -c Release

echo "build docker image"
docker build -t dbwt .

echo "push code to repo"
docker tag dbwt:latest 613727736796.dkr.ecr.eu-west-1.amazonaws.com/dbwt:latest
docker push 613727736796.dkr.ecr.eu-west-1.amazonaws.com/dbwt:latest
