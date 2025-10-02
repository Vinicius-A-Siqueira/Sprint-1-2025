using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mottu.Fleet.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IPatioRepository Patios { get; }
    IMotoRepository Motos { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
