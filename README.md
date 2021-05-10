# Project Layout:
This project loosely follows the [Clean Architecture Layout by Jason Taylor.](https://github.com/jasontaylordev/CleanArchitecture)

**Riders.Tweakbox.API**: The top layer housing the main ASP.NET Application. This is the presentation layer that exposes the functionality (API) to the outside world.

**Riders.Tweakbox.Application**: Contains all application logic. Depends on the domain layer only. This layer defines interfaces that are implemented by outside layers. For example, if the application needed to have a new service, a new interface would be added to Application and an implementation would be created within Infrastructure.

**Riders.Tweakbox.Infrastructure**: Contains the implementations of the interfaces defined in the Application project; enforcing separation of interface and implementation. This is where the platform lives (databases, I/O, web services) and accessing of external resources occurs.

**Riders.Tweakbox.Domain**: This contains all entities, enums, exceptions, interfaces, types and logic specific to them.

## Running Migrations
dotnet ef migrations add InitialCreate --project Riders.Tweakbox.API.Infrastructure --startup-project Riders.Tweakbox.API

## Ranked Match Flow
1. Host Sends Player Data and Receives Match Token (t0)
   - If token already exists, existing match is void and new one is made.
   - Host distributes token to clients.
   
2. On Match Finish:
   - Players Submit Match Results with Token.
		- If token is invalid, do nothing.
		
   - If token is valid, hash match data, and count duplicates.
	  - Dictionary<Token, List<(Hash, Count), Data>>.
	  
   - If there are enough matching submissions for 50+% of Players Marked Race Completed (check every add), add the result.
   