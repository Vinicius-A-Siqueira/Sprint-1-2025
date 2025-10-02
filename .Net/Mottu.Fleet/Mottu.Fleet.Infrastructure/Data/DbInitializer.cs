using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(FleetDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (context.Users.Any())
            return;

        var users = new[]
        {
            new User
            {
                Username = "admin",
                Password = "{noop}admin123",
                Profile = "ROLE_ADMIN",
                FullName = "Administrador do Sistema",
                Email = "admin@mottu.com",
                Phone = "(11) 99999-9999",
                Status = UserStatus.Active
            },
            new User
            {
                Username = "user",
                Password = "{noop}user123",
                Profile = "ROLE_FUNCIONARIO",
                FullName = "Funcionário Padrão",
                Email = "funcionario@mottu.com",
                Phone = "(11) 88888-8888",
                Status = UserStatus.Active
            },
            new User
            {
                Username = "operador",
                Password = "{noop}operador123",
                Profile = "ROLE_OPERADOR",
                FullName = "Operador de Campo",
                Email = "operador@mottu.com",
                Phone = "(11) 77777-7777",
                Status = UserStatus.Active
            }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        var patios = new[]
        {
            new Patio
            {
                Nome = "Patio Norte",
                Endereco = "Rua XPTO, 123",
                Cidade = "São Paulo",
                Estado = "SP",
                Cep = "01234-567",
                Capacidade = 150,
                Telefone = "(11) 3333-3333",
                Observacoes = "Pátio principal da região norte da cidade"
            },
            new Patio
            {
                Nome = "Patio Sul",
                Endereco = "Avenida ABC, 456",
                Cidade = "São Paulo",
                Estado = "SP",
                Cep = "04567-890",
                Capacidade = 200,
                Telefone = "(11) 4444-4444",
                Observacoes = "Pátio secundário da região sul"
            },
            // Outros pátios conforme necessidade...
        };

        context.Patios.AddRange(patios);
        await context.SaveChangesAsync();

        var motos = new[]
        {
            new Moto
            {
                Placa = "ABC1D23",
                Modelo = "Mottu Sport 110i",
                PatioId = patios[0].Id,
                Ano = 2023,
                Cor = "Branca",
                Quilometragem = 0,
                Status = MotoStatus.Disponivel,
                Chassi = "9BD12345678901234",
                NumeroMotor = "MT110ABC123456",
                Observacoes = "Moto nova, recém chegada da fábrica"
            },
            new Moto
            {
                Placa = "DEF4G56",
                Modelo = "Mottu Delivery 2023",
                PatioId = patios[1].Id,
                Ano = 2023,
                Cor = "Vermelha",
                Quilometragem = 1500,
                Status = MotoStatus.Disponivel,
                Chassi = "9BD12345678901235",
                NumeroMotor = "MT110DEF123456",
                Observacoes = "Moto usada em perfeito estado"
            },
            // Outras motos conforme necessidade...
        };

        context.Motos.AddRange(motos);
        await context.SaveChangesAsync();
    }
}
