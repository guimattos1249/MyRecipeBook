using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Security.Tokens;
using System.Security.Claims;
using System.Text;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access;

internal sealed class JwtTokenHandler(uint expirationTimeInMinutes, string signingKey) : IAccessTokenGenerator
{
    private readonly uint _expirationTimeInMinutes = expirationTimeInMinutes;
    private readonly string _signingKey = signingKey;

    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        var tokenDesciptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
            SigningCredentials = new SigningCredentials(Credentials(), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var handler = new JsonWebTokenHandler();
        
        return handler.CreateToken(tokenDesciptor);
    }

    private SymmetricSecurityKey Credentials()
    {
        var keyBytes = Encoding.UTF32.GetBytes(_signingKey);
        
        return new SymmetricSecurityKey(keyBytes);
    }
}
