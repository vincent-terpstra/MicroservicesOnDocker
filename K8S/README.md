```bash
    kubectl apply -f platforms-depl.yaml
```

```bash
    kubectl apply -f commands-depl.yaml
```

```bash
    kubectl get deployments
```

```bash
    kubectl get pods
```

```bash
    kubectl delete -f platforms-depl.yamls
```

```bash
    kubectl rollout restart deployment platforms-depl
```

```bash
  kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.0/deploy/static/provider/cloud/deploy.yaml
```

# Persistent volume claims
```bash
    kubectl apply -f local-pvc.yaml
```

```bash
    kubectl get pvc
```

# Database secret setup
# namespace = mssql
# key = SA_PASSWORD
```bash
    kubectl create secret generic mssql --from-literal=SA_PASSWORD=''
```

# Set PlatformDatabase connection string
```bash
    kubectl create secret generic db-credentials --from-literal=ConnectionStrings__PlatformsDatabase="Server=mssql-clusterip-srv,1433;Initial Catalog=platformsdb;User ID=sa;Password=;"
```