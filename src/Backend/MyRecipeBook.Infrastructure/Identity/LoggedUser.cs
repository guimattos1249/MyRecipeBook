using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Identity;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Infrastructure.DataAccess;

namespace MyRecipeBook.Infrastructure.Identity;

internal class LoggedUser(IAccessTokenProvider accessTokenProvider, MyRecipeBookDbContext dbContext) : ILoggedUser
{
    private readonly IAccessTokenProvider _accessTokenProvider = accessTokenProvider;
    private readonly MyRecipeBookDbContext _dbContext = dbContext;

    public async Task<User> Get()
    {
        var userId = GetUserId();

        return await _dbContext.Users.AsNoTracking().FirstAsync(u => u.Active && u.Id == userId);
    }

    public Guid GetUserId()
    {
        var accessToken = _accessTokenProvider.GetToken();

        var handler = new JsonWebTokenHandler();

        var jsonWebToken = handler.ReadJsonWebToken(accessToken);

        var sub = jsonWebToken.Subject;

        return Guid.Parse(sub);
    }
}
