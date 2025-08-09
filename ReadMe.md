# LeapEventTechWebAPI

This application is a simple event listing web API built with ASP.NET Core Web API (Target: .NET 8).
Data access uses NHibernate against a provided SQLite database. Services and repositories are wired via .NET DI. Unit tests use NUnit + Moq.

# Setup the Application

You can build and run the application using the following commands:
```bash
dot net build
dot net run
```

# Errors due to missing DB file
If you encounter any errors while testing any endpoints and error showing "missing database file", please ensure that the connection string in `appsettings.json` is correctly pointing to the right path. You can provide the full path and try again.

# Run the Tests
You can run the tests using the following command:
```bash
dotnet test
```

# TechStack
This project uses the following technologies:

- .NET 8 / ASP.NET Core Web API
- NHibernate
- SQLite (DB file included)
- NUnit for unit tests, Moq for mocking
- Swagger

# End points
The API provides the following endpoints:
- `GET /api/events`: Retrieves a list of all events.
- `GET /api/events/{eventId}/tickets` – tickets for a specific event
- `GET /api/tickets/top5?by=count|amount` – top-5 events by tickets sold or total revenue

# Approach to create this web API

1) Database setup
-Source: SQLite file provided in the assignment.
-Connection: Added a Default connection string in appsettings.json, pointing to the .db file.

2) NHibernate Configuration
- Installed NHibernate and System.Data.SQLite
- Session factory built once at startup and registered in DI; ISession injected per request (scoped).
- Entities created for Event and Ticket, with mappings defined in separate mapping classes (Events.hbm.xml and Tickets.hbm.xml).

3) Repository layer(data access layer)
- Created a generic repository interface and implementation for Events and Tickets for common CRUD operations.
- Repositories use NHibernate ISession for data access, allowing for easy querying and manipulation of entities.
- Repositories registered in the DI container for use in services.

4) Service layer (business logic)
- Created services for Events and Tickets, implementing business logic and using repositories for data access.
- Services registered in the DI container for use in controllers.

5) API endpoints (controllers)
- Created controllers for Events and Tickets, exposing endpoints for data retrieval.
- Endpoints use services to handle requests and return data in JSON format.

6) Unit tests
- Created unit tests for repositories and services using NUnit and Moq.

7) Troubleshooting
- Ensured proper NHibernate configuration and mappings.
- Verified database connection and data access by a debugcontroller in the beginning.
- Tested endpoints using Swagger UI.
- Mocked dependencies in unit tests to isolate components and verify behavior.
- Enabled CORS to allow cross-origin requests from our Angular WebAPP.
