# CEP SmartWallet

**Transaction Processing System**

A lightweight transaction processing application demonstrating clean architecture, full-stack integration, and Kubernetes orchestration.

---

## Overview

CEP SmartWallet is a sample system that:

- Processes financial transactions
- Exposes REST APIs
- Provides a simple React UI
- Runs fully containerized on Kubernetes

---

## Architecture

Clean Architecture with clear separation of concerns:

```
Domain          # Business logic
Application     # CQRS, MediatR
Infrastructure  # EF Core
API             # ASP.NET Core (.NET 10)
UI              # React (Vite)
```

**Patterns:**
- CQRS
- Repository Pattern
- Dependency Injection

---

## Backend

- .NET 10 (ASP.NET Core)
- Entity Framework Core
- MS SQL Server
- MediatR

**Features:**
- Create & query transactions
- Automatic DB migrations (with retry)
- RESTful API

---

## Frontend

- React (Vite, JavaScript)
- NGINX (containerized)

**Features:**
- Create transactions
- List transactions

---

## Docker

All components are containerized:

- `cep-smartwallet-api`
- `cep-smartwallet-ui`
- SQL Server (official image)

---

## Kubernetes

Deployed using Kubernetes:

- Namespace: `cep-smartwallet`
- API → NodePort (external)
- UI → NodePort (external)
- DB → ClusterIP (internal)

**Networking:**
- UI (inside cluster) → API via DNS (`localhost:30009`)
- UI (local dev) → API via `localhost:5000`

---

## Configuration

Environment-specific API URL:

```
# Development
VITE_API_URL=http://localhost:5000

# Kubernetes
VITE_API_URL=http://localhost:30009
```

---

## Run

### Docker
```
docker compose up --build
```

### Kubernetes
```
kubectl apply -f k8s/
```

---

## Highlights

- Clean Architecture in practice
- Full-stack containerized system
- Kubernetes service discovery
