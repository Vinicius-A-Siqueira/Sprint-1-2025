using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mottu.Fleet.Application.Interfaces;
using Mottu.Fleet.Application.Mappings;
using Mottu.Fleet.Application.Services;
using Mottu.Fleet.Domain.Interfaces;
using Mottu.Fleet.Infrastructure.Data;
using Mottu.Fleet.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<FleetDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseInMemoryDatabase("MottuFleetDb");
    }
    else
    {
        options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPatioService, PatioService>();
builder.Services.AddScoped<IMotoService, MotoService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPatioRepository, PatioRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottu Fleet Management API",
        Version = "v1",
        Description = "API para gerenciamento da frota Mottu.",
        Contact = new OpenApiContact
        {
            Name = "Equipe Mottu",
            Email = "dev@mottu.com.br",
            Url = new Uri("https://mottu.com.br")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu Fleet API v1");
        c.RoutePrefix = string.Empty;
    });

    app.UseCors("Development");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FleetDbContext>();
    await DbInitializer.InitializeAsync(dbContext);
}

app.Run();
