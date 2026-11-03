using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.API.Token;

internal sealed class HttpContextTokenProvider(IHttpContextAccessor httpContextAccessor) : IAccessTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string GetToken()
    {
        var accessToken = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return accessToken["Bearer ".Length..];
    }
}
