using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Infrastructure.Data;
public class MottuDbContext : DbContext
{
    public MottuDbContext(DbContextOptions<MottuDbContext> options) : base(options) { }

    public DbSet<Filial> Filiais { get; set; }
    public DbSet<Patio> Patios { get; set; }
    public DbSet<Moto> Motos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
