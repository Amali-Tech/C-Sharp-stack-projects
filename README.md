# C-Sharp-stack-projects

This meeting was lead by Bright Edem Gawu on March 27, 2025.

***

***

# Setting Up the Structure for a Backend API

This document outlines the steps to establish a robust and scalable backend API structure using .NET 9.0. The
architecture follows a clean, modular design with separate projects for API, application logic, infrastructure,
persistence, and domain models, complemented by comprehensive unit testing.

---

## Project Overview

- **Solution Name**: `BackendApi.sln`
- **Framework**: .NET 9.0
- **Architecture**: Clean Architecture with Domain-Driven Design (DDD) principles
- **Database**: PostgreSQL (via Entity Framework Core)
- **API Documentation**: Scalar (OpenAPI/Swagger)
- **Testing**: xUnit

The solution is organized into two main directories:

- **`src`**: Contains the core application projects.
- **`tests`**: Contains unit test projects for each layer.

---

## Step 1: Create the Solution

Initialize a new .NET solution to house all projects.

```bash
  dotnet new sln -n BackendApi
```

**Reference
**: [Microsoft Documentation on Solution Files](https://learn.microsoft.com/en-us/visualstudio/extensibility/internals/solution-dot-sln-file?view=vs-2022)

---

## Step 2: Project Structure

Create the following directory structure:

```
BackendApi/
├── src/
│   ├── Api/           # Web API project (entry point)
│   ├── Application/   # Business logic layer
│   ├── Domain/        # Core domain models
│   ├── Infrastructure/# Cross-cutting concerns
│   └── Persistence/   # Data access layer
└── tests/
    ├── Api.Tests/
    ├── Application.Tests/
    ├── Domain.Tests/
    ├── Infrastructure.Tests/
    └── Persistence.Tests/
```

Create the following folders in the solution root:

- **`src`**: For application source code.
- **`tests`**: For unit test projects.

```bash
  mkdir src tests
```

---

## Step 3: Create Core Projects

In the `src` directory, create the following .NET projects and add them to the solution. Each project serves a distinct
purpose in the architecture.

1. **`Api`**: Hosts controllers and API endpoints.
2. **`Application`**: Contains business logic and services.
3. **`Infrastructure`**: Manages external services and dependencies.
4. **`Persistence`**: Handles data access and database interactions.
5. **`Domain`**: Defines core entities and business rules.

```bash
  cd src
  dotnet new webapi -n Api --use-controllers
  dotnet new classlib -n Application
  dotnet new classlib -n Infrastructure
  dotnet new classlib -n Persistence
  dotnet new classlib -n Domain

  cd ..
  dotnet sln BackendApi.sln add src/Api/Api.csproj
  dotnet sln BackendApi.sln add src/Application/Application.csproj
  dotnet sln BackendApi.sln add src/Infrastructure/Infrastructure.csproj
  dotnet sln BackendApi.sln add src/Persistence/Persistence.csproj
  dotnet sln BackendApi.sln add src/Domain/Domain.csproj
```

---

## Step 4: Set Up Unit Testing

In the `tests` directory, create xUnit test projects corresponding to each core project and add them to the solution.

```bash
  cd tests
  dotnet new xunit -n Api.Tests
  dotnet new xunit -n Application.Tests
  dotnet new xunit -n Infrastructure.Tests
  dotnet new xunit -n Persistence.Tests
  dotnet new xunit -n Domain.Tests

  cd ..
  dotnet sln BackendApi.sln add tests/Api.Tests/Api.Tests.csproj
  dotnet sln BackendApi.sln add tests/Application.Tests/Application.Tests.csproj
  dotnet sln BackendApi.sln add tests/Infrastructure.Tests/Infrastructure.Tests.csproj
  dotnet sln BackendApi.sln add tests/Persistence.Tests/Persistence.Tests.csproj
  dotnet sln BackendApi.sln add tests/Domain.Tests/Domain.Tests.csproj
```

Run the application or tests using:

```bash
  dotnet run --project src/Api/Api.csproj
  dotnet watch --project src/Api/Api.csproj
  dotnet test BackendApi.sln
```

**Optional**: Explore Minimal APIs for lightweight
endpoints. [Learn More](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-9.0).

Example Minimal API in `Program.cs`:

```csharp
app.MapGet("/", () => "Hello World!");
```

Update `launchSettings.json` to launch the browser automatically:

```json
"launchBrowser": true
```

---

## Step 5: Add API Documentation with Scalar (OpenAPI)

Enhance API discoverability by integrating Scalar for interactive documentation.

1. Add the Scalar package to the `Api` project:

```bash
  dotnet add src/Api/Api.csproj package Scalar.AspNetCore
```

2. Update `Program.cs` in the `Api` project to enable OpenAPI and Scalar:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Backend API Documentation";
        options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(
            ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
```

3. Update `launchSettings.json` to open the Scalar UI on launch:

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "scalar/v1",
      "applicationUrl": "http://localhost:5040",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "scalar/v1",
      "applicationUrl": "https://localhost:7262;http://localhost:5040",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

Navigate to `/scalar/v1` to view the API documentation.

## Step 6: Configure CORS

Enable Cross-Origin Resource Sharing (CORS) to allow frontend applications to interact with the API.

Add the following after `app.UseHttpsRedirection()` in `Program.cs`:

```csharp
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});
```

**Note**: In production, restrict `AllowAnyOrigin` to specific domains for security.

---

## Step 7: Establish Project References

Define dependencies between projects to enforce the clean architecture.

1. **Api.csproj**:

```xml

<Project Sdk="Microsoft.NET.Sdk.Web">
    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>
    <ItemGroup>
        <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.0"/>
        <PackageReference Include="Scalar.AspNetCore" Version="2.1.2"/>
    </ItemGroup>
    <ItemGroup>
        <ProjectReference Include="..\Application\Application.csproj"/>
        <ProjectReference Include="..\Infrastructure\Infrastructure.csproj"/>
        <ProjectReference Include="..\Persistence\Persistence.csproj"/>
    </ItemGroup>
</Project>
```

2. **Application.csproj**:

```xml

<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>
    <ItemGroup>
        <ProjectReference Include="..\Domain\Domain.csproj"/>
    </ItemGroup>
</Project>
```

3. **Infrastructure.csproj**:

```xml

<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>
    <ItemGroup>
        <ProjectReference Include="..\Application\Application.csproj"/>
    </ItemGroup>
</Project>
```

4. **Persistence.csproj**:

```xml

<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>
    <ItemGroup>
        <ProjectReference Include="..\Application\Application.csproj"/>
        <ProjectReference Include="..\Domain\Domain.csproj"/>
    </ItemGroup>
</Project>
```

---

## Step 8: Implement Global Exception Handling

Standardize responses across the API with a global exception handler.

1. In `Application/DTOs`, create response DTOs:

**ApiSuccessResponse.cs**:

```csharp
/// <summary>
/// Represents a standardized success response for API requests.
/// </summary>
/// <typeparam name="T">The type of data returned.</typeparam>
/// <param name="StatusCode">The HTTP status code.</param>
/// <param name="Message">A descriptive message (default: empty).</param>
/// <param name="Data">The response data (default: null).</param>
public record ApiSuccessResponse<T>(
    int StatusCode,
    string Message = "",
    T? Data = default);
```

**ApiErrorResponse.cs**:

```csharp
/// <summary>
/// Represents a standardized error response for API requests.
/// </summary>
/// <param name="Detail">Detailed error description.</param>
/// <param name="StatusCode">The HTTP status code.</param>
/// <param name="Title">Optional error title (default: null).</param>
/// <param name="Errors">Optional additional error details (default: null).</param>
public record ApiErrorResponse(
    string Detail,
    int StatusCode,
    string? Title = null,
    IEnumerable<KeyValuePair<string, string>>? Errors = null);
```

2. In `Application/Exceptions`, create a base exception class:

**ServiceAppException.cs**:

```csharp
/// <summary>
/// Base class for custom application exceptions.
/// </summary>
public abstract class ServiceAppException : Exception
{
    protected ServiceAppException(string message) : base(message) { }

    public virtual int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
    protected virtual string? Title { get; init; }
    protected virtual IEnumerable<KeyValuePair<string, string>>? Errors { get; init; }

    public ApiErrorResponse FormatError() => new(
        Detail: Message,
        StatusCode: StatusCode,
        Title: Title,
        Errors: Errors);
}
```

3. In `Api`, create the global exception handler:

**GlobalExceptionHandler.cs**:

```csharp
/// <summary>
/// Handles exceptions globally across the API.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ServiceAppException appException)
        {
            var errorResponse = appException.FormatError();
            httpContext.Response.StatusCode = errorResponse.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
            return true;
        }

        _logger.LogError(exception, "Unexpected error: {Message}", exception.Message);
        var errorResponse = new ApiErrorResponse("Internal Server Error", StatusCodes.Status500InternalServerError);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
        return true;
    }
}
```

4. Register the handler in `Program.cs`:

```csharp
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
app.UseExceptionHandler(_ => { });
```

---

## Step 9: Add Database Support (PostgreSQL)

Integrate PostgreSQL using Entity Framework Core.

1. Add packages:

```bash
  dotnet add src/Persistence/Persistence.csproj package Npgsql.EntityFrameworkCore.PostgreSQL
  dotnet add src/Api/Api.csproj package Microsoft.EntityFrameworkCore.Design
```

2. In `Domain/Entities`, define the base entity and a sample `Todo` entity:

**BaseEntity.cs**:

```csharp
public abstract class BaseEntity
{
    public Guid Id { get; set; }
}
```

**Todo.cs**:

```csharp
public class Todo : BaseEntity
{
    public required string Name { get; set; }
}
```

3. In `Persistence`, create the database context and configurations:

**IAssemblyMarker.cs**:

```csharp
public interface IAssemblyMarker { }
```

**AppDbContext.cs**:

```csharp
public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}
```

**TodoEntityTypeConfiguration.cs**:

```csharp
public class TodoEntityTypeConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
    }
}
```

**PersistenceExtensions.cs**:

```csharp
public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }
}
```

4. Update `Program.cs` in `Api`:

```csharp
builder.Services.AddPersistence(builder.Configuration);
```

5. Update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=backend_api;Username=postgres;Password=your_password"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

6. Generate and apply migrations:

```bash
  dotnet ef migrations add InitialCreate -p src/Persistence -s src/Api
  dotnet ef database update -p src/Persistence -s src/Api
```

**Reference
**: [EF Core Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

7. Seed the database (run in PostgreSQL):

```sql
INSERT INTO "Todos" ("Id", "Name")
VALUES (gen_random_uuid(), 'Task 1'),
       (gen_random_uuid(), 'Task 2'),
-- Add more as needed
       (gen_random_uuid(), 'Task 20');
```

---

## Step 10: Implement a Sample API Endpoint

Create a simple Read endpoint for the `Todo` entity.

1. In `Application/DTOs`:

**TodoReadDto.cs**:

```csharp
public record TodoReadDto(Guid Id, string Name);
```

2. In `Application/Mappings`:

**TodoMappingExtensions.cs**:

```csharp
public static class TodoMappingExtensions
{
    public static TodoReadDto ToReadDto(this Todo todo) => new(todo.Id, todo.Name);
}
```

3. In `Application/Services`:

**ITodoService.cs**:

```csharp
public interface ITodoService
{
    Task<IEnumerable<TodoReadDto>> GetAllAsync();
}
```

**TodoService.cs**:

```csharp
public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository) => _repository = repository;

    public async Task<IEnumerable<TodoReadDto>> GetAllAsync()
    {
        var todos = await _repository.GetAllAsync();
        return todos.Select(t => t.ToReadDto());
    }
}
```

4. In `Application/ContractsPersistence`:

**ITodoRepository.cs**:

```csharp
public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAllAsync();
}
```

5. In `Persistence/Repositories`:

**TodoRepository.cs**:

```csharp
public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context) => _context = _context;

    public async Task<IEnumerable<Todo>> GetAllAsync() =>
        await _context.Todos.AsNoTracking().ToListAsync();
}
```

6. Update `PersistenceExtensions.cs`:

```csharp
private static void RegisterRepositories(IServiceCollection services)
{
    services.AddScoped<ITodoRepository, TodoRepository>();
}
```

7. In `Application`:

**ApplicationExtensions.cs**:

```csharp
public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITodoService, TodoService>();
        return services;
    }
}
```

8. Update `Program.cs` in `Api`:

```csharp
builder.Services.AddApplication(builder.Configuration);
```

9. In `Api/Controllers`:

**BaseController.cs**:

```csharp
/// <summary>
///     Represents the base controller for the API.
/// </summary>
/// <remarks>
///     This abstract controller provides common configuration and behavior for all derived API controllers.
///     It ensures consistent routing, content negotiation, and response formatting across the API.
/// </remarks>
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase { }
```

**TodosController.cs**:

```csharp
public class TodosController : BaseController
{
    private readonly ITodoService _service;

    public TodosController(ITodoService service) => _service = service;

    [ProducesResponseType(typeof(ApiSuccessResponse<<IEnumerable<TodoReadDto>>>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<ApiSuccessResponse<<IEnumerable<TodoReadDto>>> GetAll()
    {
        var todos = await _service.GetAllAsync();
        return Ok(new ApiSuccessResponse<IEnumerable<TodoReadDto>>(200, "Todos retrieved successfully", todos));
    }
}
```
