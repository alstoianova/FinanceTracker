# FinanceTracker API

FinanceTracker API is a backend application for managing personal finances.

## Features

- Create transactions
- Update transactions
- Delete transactions
- Get transaction by id
- Filter transactions
- Manage accounts
- Manage categories
- Statistics endpoint
- Swagger UI
- SQLite database
- CQRS architecture with MediatR
- Clean Architecture

## Technologies

- ASP.NET Core Minimal API
- Entity Framework Core
- SQLite
- MediatR
- Swagger
- Clean Architecture

## Endpoints

### Transactions

- GET /transactions
- GET /transactions/{id}
- POST /transactions
- PUT /transactions/{id}
- DELETE /transactions/{id}

### Accounts

- GET /accounts
- POST /accounts

### Categories

- GET /categories
- POST /categories

### Statistics

- GET /statistics

## Run project

```bash
dotnet run --project src/FinanceTracker.API
```

## Swagger

```text
http://localhost:5228/swagger
```
