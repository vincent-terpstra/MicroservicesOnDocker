docker build -t douglasvincent/commandservice ../../CommandsService/
docker build -t douglasvincent/platformservice ../../PlatformService/

docker push  douglasvincent/platformservice
docker push  douglasvincent/commandservice

kubectl rollout restart deployment commands-depl
kubectl rollout restart deployment platforms-depl