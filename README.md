# Watcher's Eye

![Static Badge](https://img.shields.io/badge/10-white?logo=dotnet&logoColor=white&labelColor=!%5BStatic%20Badge%5D&color=%23512BD4)
![Static Badge](https://img.shields.io/badge/Blazor-white?logo=Blazor&logoColor=white&labelColor=!%5BStatic%20Badge%5D&color=%23512BD4)
![Static Badge](https://img.shields.io/badge/GitHub%20Actions-%232088FF?logo=githubactions&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=white)
![Static Badge](https://img.shields.io/badge/Watchtower-%23416271?logo=Watchtower)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![Static Badge](https://img.shields.io/badge/Nginx%20Proxy%20Manager-%23F15833?logo=nginxproxymanager&logoColor=white)
![Discord](https://img.shields.io/badge/Discord_Webhook-5865F2?style=flat&logo=discord&logoColor=white)

**Live Demo:** [https://watcher.dahlmann.dev](https://watcher.dahlmann.dev)
___
## Project Overview
Watcher's Eye is a full-stack web application designed to track and sync character progression, deaths and ladder rankings in the game Path of Exile.

While the domain is gaming, the underlying architecture was built to demonstrate backend engineering, cloud-native deployment, and structural design patterns. It focuses heavily on data synchronization, separation of concerns, and automated infrastructure.
___
## Features
* **Live Ladder Tracking:**  
  Periodically fetches and updates character data from the official Path of Exile API.
  
* **Discord Integrations:**  
  Dispatches webhooks based on Domain Events (e.g., when a character levels up or dies in Hardcore).
  
* **Dual-Environment Strategy:**  
  Production and Demo environments run as separate Docker Compose projects with isolated virtual networks and databases, allowing true-to-production testing and the ability to trigger edge-case scenarios via a dedicated mocked GGG API in the demo environment.
___
## Architecture
The project follows _Clean Architecture/Domain Driven Design_ principles to ensure that business logic is entirely independent of UI, databases, and external APIs.

#### Project Structure
* `.Domain`: Core business entities.
* `.Application`: Business logic, CQRS Handlers, service and repository interface definitions.
* `.Infrastructure`: EF Core DbContext, repositories, services, Refit API clients.
* `.Api`: Orchestration layer for the backend, hosting Minimal APIs and the background sync worker.
* `.Client`: Blazor WebAssembly frontend.
* `.Shared`: Shared DTOs used across the network boundary by both Client and API.
___
## Tech Stack
__Backend & Frontend__
* `C# / .NET 10`
* `Minimal APIs`
* `Entity Framework Core`
* `PostgreSQL`
* `Blazor WebAssembly`

__DevOps & Infrastructure__
* `Docker`
* `Docker Compose`
* `GitHub Actions`
* `GitHub Container Registry`
* `Watchtower`
* `Nginx Proxy Manager`
___
## CI/CD Pipeline & Deployment
The application is fully containerized and hosted on a live Ubuntu Virtual Private Server (VPS). Deployments are 100% automated:

1. **Commit & Push:**  
Code is pushed to the `master` branch.

2. **Build & Publish:**  
A GitHub Actions workflow (`docker-publish.yml`) triggers, building the Client and API Docker images and pushing them to the GitHub Container Registry.

3. **Scoped Automated Rollout:**  
Two independent instances of Watchtower monitor the GitHub Container Registry. Using Docker Scopes, they ensure that updates are deployed independently:
   - `Demo Watchtower`: Immediate updates for rapid testing of new features.
   - `Live Watchtower`: Controlled updates for the production environment.
___

###### This product isn't affiliated with or endorsed by Grinding Gear Games in any way.
