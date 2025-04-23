
# C# Stack Projects

## Project Overview

This repository serves as the foundation for a collaborative project developed in tandem with our C# stack meetings. Our aim is to create a practical, real-world application while exploring and applying modern C# development techniques and best practices.

---

## 📁 Branch: `project-structure-setup-2025-03-27`

- 🔗 **View Branch**: [project-structure-setup-2025-03-27](https://github.com/Amali-Tech/C-Sharp-stack-projects/tree/project-structure-setup-2025-03-27)  
- 📅 **Meeting Date**: March 27, 2025  
- 🛠️ **Description**: This branch establishes the initial backend project structure with the following key components:  
  - Created `BackendApi.sln` solution targeting the .NET 9.0 framework.  
  - Organized project structure under `src/` (Api, Application, Domain, Infrastructure, Persistence) and `tests/` (unit test projects).  
  - Initialized core projects using `dotnet new` and linked them within the solution.  
  - Configured unit testing across all layers using xUnit.  
  - Integrated Scalar for API documentation, leveraging OpenAPI/Swagger standards.  
  - Enabled CORS to support seamless frontend integration.  
  - Defined project references to uphold clean architecture dependency rules.  
  - Implemented global exception handling with standardized DTOs for success/error responses and a custom exception base class.  

---

## 📁 Branch: `project-structure-setup-continued-2025-04-10`

- 🔗 **View Branch**: [project-structure-setup-continued-2025-04-10](https://github.com/Amali-Tech/C-Sharp-stack-projects/tree/project-structure-setup-continued-2025-04-10)  
- 📅 **Meeting Date**: April 10, 2025  
- 🛠️ **Description**: This branch builds upon the initial setup by completing the backend API implementation with the following additions:  
  - Integrated PostgreSQL using Entity Framework Core, including migrations and database seeding.  
  - Defined domain entities (`BaseEntity`, `Todo`) and configured the database context.  
  - Implemented the repository pattern for data access (`ITodoRepository`, `TodoRepository`).  
  - Added a service layer for business logic (`ITodoService`, `TodoService`).  
  - Created DTOs and mapping extensions for consistent data transfer.  
  - Developed a sample `TodosController` with a GET endpoint for retrieving todos.  
  - Extended service registration for dependency injection.  
  - Ensured adherence to clean architecture principles throughout the implementation.  

