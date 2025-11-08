using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using Mottu.Fleet.Infrastructure.Configuration;
using Mottu.Fleet.Infrastructure.Data;
using Mottu.Fleet.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Registrar IMongoClient como Singleton
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration["MongoDbSettings:ConnectionString"];
    return new MongoClient(connectionString);
});

// MongoDbContext
builder.Services.AddSingleton<MongoDbContext>();

// Repositórios
builder.Services.AddScoped<MotoMongoRepository>();
builder.Services.AddScoped<PatioMongoRepository>();
builder.Services.AddScoped<UsuarioMongoRepository>();

// Health Checks - VERSÃO CORRIGIDA COM LAMBDA
builder.Services.AddHealthChecks()
    .AddMongoDb(
        sp => new MongoClient(builder.Configuration["MongoDbSettings:ConnectionString"]!),
        name: "mongodb",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "db", "mongodb" },
        timeout: TimeSpan.FromSeconds(3));

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger com versionamento
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Mottu Fleet API",
        Version = "v1",
        Description = "API de gerenciamento de frotas - Versão 1 (Oracle)"
    });

    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Mottu Fleet API",
        Version = "v2",
        Description = "API de gerenciamento de frotas - Versão 2 (MongoDB)"
    });
});

var app = builder.Build();

// Testar conexão MongoDB ao iniciar
using (var scope = app.Services.CreateScope())
{
    try
    {
        var mongoContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
        var isConnected = await mongoContext.TestConnectionAsync();

        if (isConnected)
        {
            Console.WriteLine("✅ MongoDB conectado com sucesso!");
        }
        else
        {
            Console.WriteLine("❌ Falha ao conectar com MongoDB!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao conectar MongoDB: {ex.Message}");
    }
}

// Health Check Endpoint
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu Fleet API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Mottu Fleet API v2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
