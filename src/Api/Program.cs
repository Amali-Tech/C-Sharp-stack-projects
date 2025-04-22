using Api;
using Application.Extensions;
using Persistence.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Backend API Documentation";
        options.Theme = ScalarTheme.Mars;
        options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(
            ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello Welcome to ASP.NET Core!");

app.Run();