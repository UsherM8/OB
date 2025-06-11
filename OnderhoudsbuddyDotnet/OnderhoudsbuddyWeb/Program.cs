using Business.ContainerInterfaces;
using Business.Containers;
using Business.Mappers;
using Dal;
using Dal.Repositories;
using Dal.Services;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(CarMapper));
builder.Services.AddScoped<ICar, CarRepository>();
builder.Services.AddScoped<ICarContainer, CarContainer>();

builder.Services.AddScoped<IGarage, GarageRepository>();
builder.Services.AddScoped<IGarageContainer, GarageContainer>();
builder.Services.AddScoped<GarageMapper>();

builder.Services.AddScoped<IService, ServiceRepository>();
builder.Services.AddScoped<IServiceContainer, ServiceContainer>();
builder.Services.AddScoped<ServiceMapper>();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();


var env = builder.Environment.EnvironmentName;
Console.WriteLine($"Environment: {env}");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OnderhoudsbuddyDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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



builder.Services.AddHttpClient<RdwApiClient>(client =>
{
    client.BaseAddress = new Uri("https://opendata.rdw.nl/resource/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OnderhoudsbuddyDbContext>();
    db.Database.Migrate();
}


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
