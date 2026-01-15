using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IUserRepository
{
    IUnitOfWork UnitOfWork { get; }

    Task AddAsync(User user, CancellationToken cancellationToken);
}