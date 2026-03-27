# GTMotive Microservice Technical Test - Renting Management

This repository contains the implementation of a **Renting Management Microservice** developed as a technical assessment. The service manages a vehicle fleet, allowing operations such as registration, listing, renting, and returning vehicles, while enforcing specific business constraints.

---

## 1. Project Overview

The microservice provides a REST API to manage a renting company's operations. It is built following modern architectural patterns (Clean Architecture / Hexagonal) to ensure scalability, maintainability, and testability.

### Key Features
*   **Fleet Management**: Create new vehicles and list available units.
*   **Rental Workflow**: Rent and return vehicles through REST endpoints.
*   **Business Constraints**:
    *   **Single Rental Policy**: A user cannot rent more than one vehicle simultaneously.
    *   **Fleet Age Limit**: Vehicles manufactured more than 5 years ago are not permitted in the fleet.

---

## 2. Tech Stack

*   **Framework**: .NET 9 / Core
*   **Architecture**: Clean Architecture (Domain-Driven Design principles)
*   **Patterns**: Repository Pattern, Dependency Injection, Result Pattern.
*   **Documentation**: Swagger / OpenAPI
*   **Containerization**: Docker & Docker Compose

---

## 3. Getting Started

The project is designed to run locally without installing external dependencies (SQL Server, SDKs, etc.) other than Docker.

### Prerequisites
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running.

### Execution
1. Clone the repository.
2. Open a terminal in the root folder.
3. Run the following command:
   ```bash
   docker-compose up --build

### Accessing the API

Once the containers are healthy, you can access the interactive documentation:

*   **Swagger UI**: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 4. Testing Strategy

The solution is architectured to facilitate automated testing across different levels. Examples of each type have been implemented:

### Infrastructure Tests (API/Host)
Validates the REST layer, specifically endpoint routing and model validation (Input DTOs), without executing full business logic.

*   **Example**: Validating that the `CreateVehicle` endpoint returns a `400 Bad Request` if the model is invalid.

### Unit Tests
Validates core business logic in isolation by mocking all external dependencies.

*   **Example**: Ensuring the `Vehicle` entity correctly identifies if it is older than 5 years.

### Functional / Integration Tests
Validates the interaction between different layers (Application, Domain, and Persistence) excluding the Web Host.

*   **Example**: Simulating a full "Rent Vehicle" flow and verifying the state change in the persistence layer.

### To run tests locally:

```bash
dotnet test
```

---

## 5. Architectural Decisions

*   **Validation**: Implementation of a guard clause to ensure the "5-year rule" for vehicles at the domain level.
*   **Concurrency**: Business logic implemented to prevent a single user from having multiple active rentals.
*   **Persistence**: Uses an In-Memory database (or SQL container) for the technical test to ensure "zero-config" execution.

---

## 6. Contact

**Jorge Gutiérrez**  
*Technical Test - GTMotive Microservice*
