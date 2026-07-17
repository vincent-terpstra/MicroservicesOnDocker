# Microservices On Docker

This repository contains a demo application organized as a learning-friendly reference project. It is intended to demonstrate how an application can be structured alongside supporting infrastructure resources such as Kubernetes manifests and database persistence configuration.

## Course Reference

This project was created while following an original YouTube course.

> Original course: [.Net Microservices - full course](https://www.youtube.com/watch?v=DgVjEo3OGBI)  
> Course author/channel: [Les Jackson](https://www.youtube.com/@binarythistle)

## Project Purpose

The purpose of this repository is to provide a clean, presentable example of a demo application with supporting deployment assets.

It can be used as a reference for:

- Organizing application source code
- Separating infrastructure files from application logic
- Including Kubernetes resources in a project repository
- Documenting project layout for future contributors
- Demonstrating a simple app-plus-database structure

## Project Structure
```text
├── README.md 
├── CommandsService/ 
│ └── <application-source-files>
├── PlatformsService/ 
│ └── <application-source-files> 
├── k8s/ 
│ ├── <application-manifests> 
│ ├── <database-manifests> 
│ └── <storage-manifests> 
```

## Directory Guide

### `src/`

Contains the main application source code.

This directory is intended for:

- Application logic
- API or UI code
- Domain models
- Configuration files used by the application
- Application-specific dependencies

### `k8s/`

Contains Kubernetes resource definitions used to describe the application environment.

This directory may include manifests for:

- Application deployments
- Application services
- SQL Server or database resources
- Persistent volume claims
- Configuration maps
- Secrets templates
- Storage-related resources

Sensitive values should not be committed directly. Use placeholder values or secret management where appropriate.

### YAML File Guide

| File | Purpose |
| --- | --- |
| `platforms-depl.yaml` | Defines the Platform Service deployment and its internal Kubernetes service. This is the main service responsible for platform-related API functionality. |
| `commands-depl.yaml` | Defines the Command Service deployment and its internal Kubernetes service. This service handles command-related API functionality. |
| `ingress-srv.yaml` | Defines the ingress gateway rules used to route external HTTP traffic to the appropriate internal services. |
| `mssql-plat-depl.yaml` | Defines the Microsoft SQL Server deployment and its related Kubernetes service for database access inside the cluster. |
| `local-pvc.yaml` | Defines the persistent volume claim used to provide storage for the SQL Server database data. |
| `rabbitmq-depl.yaml` | Defines the RabbitMQ deployment and related Kubernetes services used for message-based communication between services. |

## Repository Notes

This repository is intended for demo and educational purposes.

Before using this structure in a production environment, consider adding:

- Environment-specific configuration
- Secure secret handling
- Automated tests
- CI/CD workflows
- Health checks
- Resource limits
- Backup and restore documentation
- Observability and logging guidance

## Credits

This project is based on concepts and exercises from the following YouTube course:

- *.NET Microservices* by [Les Jackson](https://www.youtube.com/@binarythistle)
- [.NET Microservices](https://www.youtube.com/watch?v=DgVjEo3OGBI)

Additional modifications, notes, and structure may have been added for demonstration and learning purposes.

## License
MIT License
