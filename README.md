# Role-Based Authorization System (.NET 10)

A robust **role-based authorization system** designed for applications where **resources** (projects, organizations, boards, work items, etc.) have **members**, and each member has a **role** with associated **permissions**. The system ensures that **every action on a resource is validated against the member's role and permissions**, providing fine-grained access control.

This system is highly **extensible** and can be easily adapted to multiple resources. For improved performance, you can integrate **Redis caching** to reduce database hits.

---

## Features

- Role-based authorization per resource
- Fine-grained permission checks for each action
- Extensible to multiple resource types (Project, Organization, WorkItems, Boards, etc.)
- Easy integration with caching mechanisms like Redis
- Strong typing with wrapper types
- Consistent error handling via **Domain Errors** and **Global Exception Handling**
- Clean and maintainable architecture following **Clean Architecture** and **DDD principles**
- Validation at the domain level to enforce business rules

---

## Tech Stack

- **.NET 10** – Clean Architecture & Domain-Driven Design (DDD)  
- **Result Pattern** – Functional style error handling  
- **Minimal APIs** – Lightweight and fast endpoints  
- **Global Exception Handling** – Centralized error management  
- **Wrapper/Strong Types** – Safer and more readable code  
- **Domain Validation & Errors** – Enforces business logic consistently  
- **Entity Framework Core** – Database access with PostgreSQL  
- **Docker Compose** – Easy setup of database and service  

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- PostgreSQL
- Docker (optional, for running PostgreSQL via Docker Compose)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/<your-username>/role-based-auth-dotnet.git
   cd role-based-auth-dotnet
   ```

2. (Optional) Go to the `.env` file in the root folder and place your configurations accordingly.

3. Run locally via Docker Compose:
   ```bash
   docker-compose up -d
   ```

---

## Extending the System

- **Add new resources:** Define the resource and associate roles and permissions.
- **Caching:** Integrate Redis caching to optimize permission lookups.
- **Custom roles/permissions:** Insert new roles and permissions in their respective tables.

