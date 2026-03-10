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
    kubectl delete -f platforms-depl.yamlls
```

```bash
    kubectl rollout restart deployment platforms-depl
```

```bash
  kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.0/deploy/static/provider/cloud/deploy.yaml
```