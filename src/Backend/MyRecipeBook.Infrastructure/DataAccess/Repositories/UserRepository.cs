using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

internal sealed class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext;
    public UserRepository(MyRecipeBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistsActiveUserWithEmail(string email) =>
        await _dbContext.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);

    public async Task<bool> ExistsActiveUserWithId(Guid userId) =>
        await _dbContext.Users.AnyAsync(u => u.Id.Equals(userId) && u.Active);

    public async Task<User?> GetByEmail(string email) =>
        await _dbContext.Users
        .AsNoTracking()
        .SingleOrDefaultAsync(u => u.Active && u.Email.Equals(email));

    public async Task UpdatePassword(Guid userId, string passwordHash)
    {
        await _dbContext
            .Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(u => u.Password, passwordHash));
    }

    public void UpdateProfile(User user)
    {
        _dbContext.Users.Attach(user);

        _dbContext.Entry(user).Property(user => user.Name).IsModified = true;
        _dbContext.Entry(user).Property(user => user.Email).IsModified = true;
    }
}
