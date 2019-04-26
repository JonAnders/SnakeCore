Param([Parameter(Mandatory=$true)][string]$version)

docker build --tag snakecore:$version .
docker tag snakecore:$version snakecore:latest
docker tag snakecore:$version jonanders/snakecore:$version
docker tag snakecore:$version jonanders/snakecore:latest

docker push jonanders/snakecore:$version
docker push jonanders/snakecore:latest
