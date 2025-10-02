using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Infrastructure.Data;
public class FleetDbContext : DbContext
{
    public FleetDbContext(DbContextOptions<FleetDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Patio> Patios { get; set; }
    public DbSet<Moto> Motos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("USUARIO");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).HasColumnName("ID");
            entity.Property(u => u.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(100);
            entity.Property(u => u.Password).HasColumnName("PASSWORD").IsRequired().HasMaxLength(100);
            entity.Property(u => u.Profile).HasColumnName("PERFIL").IsRequired().HasMaxLength(50);
            entity.Property(u => u.FullName).HasColumnName("FULL_NAME").HasMaxLength(150);
            entity.Property(u => u.Email).HasColumnName("EMAIL").HasMaxLength(150);
            entity.Property(u => u.Phone).HasColumnName("PHONE").HasMaxLength(20);
            entity.Property(u => u.Status).HasColumnName("STATUS").HasConversion<int>();
            entity.Property(u => u.LastLogin).HasColumnName("LAST_LOGIN");
            entity.Property(u => u.CreatedAt).HasColumnName("CREATED_AT");
            entity.Property(u => u.UpdatedAt).HasColumnName("UPDATED_AT");
            entity.Property(u => u.IsActive).HasColumnName("IS_ACTIVE").HasDefaultValue(true);
            entity.HasIndex(u => u.Username).IsUnique();
            entity.HasIndex(u => u.Email);
        });

        modelBuilder.Entity<Patio>(entity =>
        {
            entity.ToTable("PATIO");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("ID");
            entity.Property(p => p.Nome).HasColumnName("NOME").IsRequired().HasMaxLength(100);
            entity.Property(p => p.Endereco).HasColumnName("ENDERECO").IsRequired().HasMaxLength(255);
            entity.Property(p => p.Cidade).HasColumnName("CIDADE").HasMaxLength(100);
            entity.Property(p => p.Estado).HasColumnName("ESTADO").HasMaxLength(2);
            entity.Property(p => p.Cep).HasColumnName("CEP").HasMaxLength(10);
            entity.Property(p => p.Capacidade).HasColumnName("CAPACIDADE").HasDefaultValue(100);
            entity.Property(p => p.Telefone).HasColumnName("TELEFONE").HasMaxLength(20);
            entity.Property(p => p.Observacoes).HasColumnName("OBSERVACOES").HasMaxLength(500);
            entity.Property(p => p.CreatedAt).HasColumnName("CREATED_AT");
            entity.Property(p => p.UpdatedAt).HasColumnName("UPDATED_AT");
            entity.Property(p => p.IsActive).HasColumnName("IS_ACTIVE").HasDefaultValue(true);
            entity.HasIndex(p => p.Nome);
            entity.HasIndex(p => p.Cidade);
        });

        modelBuilder.Entity<Moto>(entity =>
        {
            entity.ToTable("MOTO");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).HasColumnName("ID");
            entity.Property(m => m.Placa).HasColumnName("PLACA").IsRequired().HasMaxLength(20);
            entity.Property(m => m.Modelo).HasColumnName("MODELO").IsRequired().HasMaxLength(100);
            entity.Property(m => m.PatioId).HasColumnName("PATIO_ID").IsRequired();
            entity.Property(m => m.Ano).HasColumnName("ANO");
            entity.Property(m => m.Cor).HasColumnName("COR").HasMaxLength(50);
            entity.Property(m => m.Quilometragem).HasColumnName("QUILOMETRAGEM").HasDefaultValue(0);
            entity.Property(m => m.Status).HasColumnName("STATUS").HasConversion<int>().HasDefaultValue(MotoStatus.Disponivel);
            entity.Property(m => m.UltimaManutencao).HasColumnName("ULTIMA_MANUTENCAO");
            entity.Property(m => m.ProximaManutencao).HasColumnName("PROXIMA_MANUTENCAO");
            entity.Property(m => m.Observacoes).HasColumnName("OBSERVACOES").HasMaxLength(500);
            entity.Property(m => m.Chassi).HasColumnName("CHASSI").HasMaxLength(50);
            entity.Property(m => m.NumeroMotor).HasColumnName("NUMERO_MOTOR").HasMaxLength(50);
            entity.Property(m => m.CreatedAt).HasColumnName("CREATED_AT");
            entity.Property(m => m.UpdatedAt).HasColumnName("UPDATED_AT");
            entity.Property(m => m.IsActive).HasColumnName("IS_ACTIVE").HasDefaultValue(true);

            entity.HasOne(m => m.Patio)
                  .WithMany(p => p.Motos)
                  .HasForeignKey(m => m.PatioId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(m => m.Placa).IsUnique();
            entity.HasIndex(m => m.PatioId);
            entity.HasIndex(m => m.Status);
            entity.HasIndex(m => m.Chassi);
        });
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                if (entry.State == EntityState.Added)
                    baseEntity.CreatedAt = DateTime.UtcNow;

                baseEntity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}

