# Development Guidelines

## Project Layout: Key Components
This project loosely follows the [Clean Architecture Layout by Jason Taylor.](https://github.com/jasontaylordev/CleanArchitecture)

**Riders.Tweakbox.API**: The top layer housing the main ASP.NET Application. This is the presentation layer that exposes the functionality (API) to the outside world.

**Riders.Tweakbox.Application**: Contains all application logic. Depends on the domain layer only. This layer defines interfaces that are implemented by outside layers. For example, if the application needed to have a new service, a new interface would be added to Application and an implementation would be created within Infrastructure.

**Riders.Tweakbox.Infrastructure**: Contains the implementations of the interfaces defined in the Application project; enforcing separation of interface and implementation. This is where the platform lives (databases, I/O, web services) and accessing of external resources occurs.

**Riders.Tweakbox.Domain**: This contains all entities, enums, exceptions, interfaces, types and logic specific to them.

## Project Layout: Misc Components

**Riders.Tweakbox.API.Application.Commands:** Provides models for all requests and responses that can be made through the API.

**Riders.Tweakbox.API.Application.SDK:** First party library to integrate/use the API.

## Running Migrations
dotnet ef migrations add InitialCreate --project Riders.Tweakbox.API.Infrastructure --startup-project Riders.Tweakbox.API