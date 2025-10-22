# MadLab.DaylightSavingsNotifier

A robust .NET 8 application for managing and notifying about Daylight Saving Time (DST) changes across time zones. Built with clean architecture, SOLID principles, and modern .NET best practices.

## Project Structure

- **Domain Layer**: Core business entities and rules.
- **Application Layer**: Business logic and orchestration (`TimeZoneNotifierService`).
- **Infrastructure Layer**: Data access and external integrations (Entity Framework Core).
- **WebAPI Layer**: API endpoints and background workers (`DSTNotificationWorker`).

## Features

- **Time Zone DST Management**: Automatically updates and scans time zones for DST changes.
- **Notification System**: Creates notifications for time zones requiring DST updates.
- **Background Processing**: Uses hosted services to run scheduled tasks (e.g., daily at 3 AM).
- **Entity Framework Core and SQLite**: Used for data access and entity tracking.

## Setup Instructions

1. **Clone the Repository**
   ```sh
   git clone https://github.com/alfcanres/MadLab.DaylightSavingsNotifier.git
   ```

2. **Install .NET 8 SDK**
   - Download and install from [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

3. **Restore Dependencies**
   ```sh
   dotnet restore
   ```

4. **Update Database (if using EF Core)**
   ```sh
   dotnet ef database update
   ```

5. **Run the Application**
   ```sh
   dotnet run --project DSTN.WebAPI
   ```

## Usage

- The application runs a background worker (`DSTNotificationWorker`) that:
  - Updates DST information for observed time zones.
  - Scans for time zones needing notification.
  - Creates notifications as needed.
- All operations are logged for monitoring and troubleshooting.

## Testing

- Unit tests are located in `DSTN.Application.Tests\Services\TimeZoneNotifier\TimeZoneNotifierServiceTests.cs`.
- To run tests:
  ```sh
  dotnet test
  ```

## Design Principles

- **Clean Architecture**: Separation of concerns between layers.
- **SOLID Principles**: Each class and service has a single responsibility and depends on abstractions.
- **Dependency Injection**: Ensures maintainability and testability.

## Configuration

- Environment-specific settings are managed via ASP.NET Core configuration system.
- Logging is implemented using the built-in logging provider; can be extended with Serilog or others.

## Project Setup
1 - Ensure you have the necessary tools installed:
	- Visual Studio 2022 or later / Visual Studio Code
	- Docker Desktop (For testing purposes)
2 - Clone the repository:
	- git clone https://github.com/alfcanres/MadLab.DaylightSavingsNotifier.git
3 - Set up the following projects as startup projects in your IDE:
	- DSTN.WebAPI
	- DSTN.AdminApp.Winforms
4 - Build the solution to restore all NuGet packages and compile the code.
5 - Run the projects.
6 - Have fun!


## Deployment

- Open CMD or terminal in the project root directory.
- Run the following command to build the Docker image:
 ```sh
	docker build -t dstn-webapi -f DSTN.WebAPI/Dockerfile .
  ```
- Once the image is built, open Docker Desktop and run a new container from the `dstn-webapi` image.
- Go to MadLab.DaylightSavingsNotifier\DSTN.AdminApp.WinForms\App.config and modify the connection string to point to the database hosted in the Docker container.
- Run the WinForms application to interact with the API.


## Contributing

1. Fork the repository.
2. Create a feature branch.
3. Commit your changes following the project's coding standards.
4. Submit a pull request.

## License

This project is licensed under the MIT License.

---

**For more details, see the code comments and documentation in each layer.**
