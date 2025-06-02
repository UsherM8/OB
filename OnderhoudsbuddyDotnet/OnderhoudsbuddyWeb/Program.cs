using Business.ContainerInterfaces;
using Business.Containers;
using Business.Mappers;
using Dal;
using Dal.Repositories;
using Dal.Services;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// AutoMapper + Containers
builder.Services.AddAutoMapper(typeof(CarMapper));
builder.Services.AddScoped<ICar, CarRepository>();
builder.Services.AddScoped<ICarContainer, CarContainer>();

builder.Services.AddScoped<IGarage, GarageRepository>();
builder.Services.AddScoped<IGarageContainer, GarageContainer>();
builder.Services.AddScoped<GarageMapper>();

builder.Services.AddScoped<IService, ServiceRepository>();
builder.Services.AddScoped<IServiceContainer, ServiceContainer>();
builder.Services.AddScoped<ServiceMapper>();

// Select correct connection string based on environment
var env = builder.Environment.EnvironmentName;
Console.WriteLine($"Environment: {env}");

var config = builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env}.json", optional: true)
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OnderhoudsbuddyDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Swagger + CORS
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// External API
builder.Services.AddHttpClient<RdwApiClient>(client =>
{
    client.BaseAddress = new Uri("https://opendata.rdw.nl/resource/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Test"))
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();
