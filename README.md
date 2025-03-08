# Inventory System

This project is an inventory management system designed to apply the concepts I learn during my university studies at [Örebro University](https://www.oru.se/), by building a functional solution. It serves as a practical application to enhance my skills in system development, utilizing technologies such as C# ASP.NET, T-SQL, Dapper, and a three-tier architecture.

## Features

- Management of products in the inventory.
- Inventory status and quantities.
- API endpoints for managing products, including adding, updating, and deleting.

## Architecture

The project is structured according to a three-tier architecture:

- **WebApi**: Uses ASP.NET Web API to expose RESTful services.
- **Services**: Contains business logic and communicates with the Repository layer.
- **Repository**: Handles data access and interacts with the database using Dapper.

## Technologies

- **C#** and **ASP.NET** for backend and API development.
- **SQL Server** with **T-SQL** for database storage - see [resources](./resources).
- **Dapper** for database access.
- **Entity-Relationship Diagram** for database design - see [resources](./resources).
- **Docker** for modularity and platform independence.

## License

This project is licensed under the MIT License - see [LICENSE](./LICENSE) for more information.
