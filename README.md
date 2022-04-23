# Payment Gateway API

API-based application that allows a merchant to offer a way for their shoppers to pay for their product. Responsible for validating requests, storing card information and forwarding payment requests, and accepting payment responses to and from the acquiring bank.
## Run the Project

You will need the following tools:

* [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs) or [Rider](https://www.jetbrains.com/rider/)

### Local run

1. Clone the repository.
2. Open solution with one of the IDE.
3. Set PaymentGateway.API as startup project.
4. Start.

## Project overview

* Implemented DDD, CQRS and Clean Architecture patterns
* CQRS is implemented with using MediatR, FluentValidation and AutoMApper packages
* Using EntityFrameworkCore/EntityFrameworkCore.InMemory to impelment data access layer (because in-memory database is used all data would gone after application restart).

### Solution Structure

* PaymentGateway.API - API endpoints for the Payment Gateway
* PaymentGateway.Application - API contracts commands, queries and handlers
* PaymentGateway.Domain - domain entities models (DDD pattern)
* PaymentGateway.Infrastructure - data access layer, repositories and other services implementation
* PaymentGateway.UnitTests - unit tests project

## Opensource Tools Used

* Automapper (for object-to-object mapping)
* FluentValidation (to implement strongly-typed validation rules)
* MediatR (for request/response, commands, queries notifications dispatching)
* Moq (mocking framework)
* Entity Framework (EntityFrameworkCore.InMemory for Data Access simulation)

## Areas of improvement

1. Implement authentication
2. Improve model validation with according to required business cases
3. Add retry logic for the cases when Acquiring Bank returns unsuccess result
4. Implement card details data encryption when save it to the database
5. Add connection to the real database (AWS DynamoDB or MongoDB)
6. Add additional logging and monitoring
7. Add API documentation
8. Add more unit and integration tests
9. Add docker build and publish support
10. Add CI/CD to deploy service to cloud container
