using Microsoft.EntityFrameworkCore;
using Mottu.Fleet.Infrastructure.Data;
using Mottu.Fleet.Application.Interfaces;
using Mottu.Fleet.Application.Services;
using AutoMapper;
using Mottu.Fleet.Application.Mapping;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configurar Kestrel para escutar em todas as interfaces na porta 5000
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5000); // Permite conexões externas
        });

        var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
        builder.Services.AddDbContext<MottuDbContext>(opt =>
            opt.UseOracle(connectionString));

        builder.Services.AddScoped<IMotoService, MotoService>();
        builder.Services.AddScoped<IFilialService, FilialService>();
        builder.Services.AddScoped<IPatioService, PatioService>();

        builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu Fleet API v1");
            c.RoutePrefix = "swagger"; // Acessível em http://localhost:5000/swagger
        });
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
