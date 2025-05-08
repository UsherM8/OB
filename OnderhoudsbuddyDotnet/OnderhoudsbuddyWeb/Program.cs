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
builder.Services.AddScoped<ICar, CarRepository>();
builder.Services.AddScoped<ICarContainer, CarContainer>();
builder.Services.AddAutoMapper(typeof(CarMapper));
builder.Services.AddDbContext<OnderhoudsbuddyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<RdwApiClient>(client =>
{
    client.BaseAddress = new Uri("https://opendata.rdw.nl/resource/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();