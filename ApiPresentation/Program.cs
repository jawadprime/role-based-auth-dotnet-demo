using ApiPresentation.Bootstrap;
using Infrastructure.Persistence;
using Infrastructure.Persistence.DbEntities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Bootstrap;
using MinimalApi.Middlewares;
using MinimalApi.V1.Projects;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler(options => { });

await DatabaseInitializer.InitializeAsync(app.Services);

app.UseHttpsRedirection();

app.MapProjectEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();