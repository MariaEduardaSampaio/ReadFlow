using Application.Abstractions.Persistence;
using Domain.Entities;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(ReadFlowDbContext dbContext): IUserRepository
{
    public IUnitOfWork UnitOfWork => dbContext;
    
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}