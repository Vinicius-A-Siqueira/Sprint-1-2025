using System;
using System.Threading;
using System.Threading.Tasks;
using Mottu.Fleet.Domain.Interfaces;
using Mottu.Fleet.Infrastructure.Data;

namespace Mottu.Fleet.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly FleetDbContext _context;

    public IUserRepository Users { get; }
    public IPatioRepository Patios { get; }
    public IMotoRepository Motos { get; }

    public UnitOfWork(FleetDbContext context, IUserRepository users, IPatioRepository patios, IMotoRepository motos)
    {
        _context = context;
        Users = users;
        Patios = patios;
        Motos = motos;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync() =>
        await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync() =>
        await _context.Database.CommitTransactionAsync();

    public async Task RollbackTransactionAsync() =>
        await _context.Database.RollbackTransactionAsync();

    public void Dispose() => _context.Dispose();
}

