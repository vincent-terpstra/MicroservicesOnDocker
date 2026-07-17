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
├── src/ 
│ └── <application-source-files> 
├── k8s/ 
│ ├── <application-manifests> 
│ ├── <database-manifests> 
│ └── <storage-manifests> 
├── docs/ 
│ └── <project-documentation> 
├── scripts/ 
│ └── <helper-scripts> 
└── .gitignore
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

### `docs/`

Contains supporting documentation for the project.

This directory can be used for:

- Architecture notes
- Course notes
- Screenshots
- Design decisions
- Diagrams
- Additional setup references

### `scripts/`

Contains helper scripts related to development, maintenance, or demonstrations.

Examples include:

- Build helpers
- Cleanup helpers
- Formatting utilities
- Local development helpers

Scripts should be documented clearly before use.

## Suggested Documentation Additions

To make this demo project more complete, consider adding:

- A short architecture diagram
- Screenshots of the running application
- Notes from the original course
- A list of technologies used
- A brief explanation of the database design
- A summary of Kubernetes resources included in the project

## Technologies

This project may include or reference the following technologies:

- Application framework: `<application-framework>`
- Database: Microsoft SQL Server
- Containerization: Docker
- Orchestration: Kubernetes
- Deployment assets: Kubernetes YAML manifests

Update this section as the project evolves.

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
