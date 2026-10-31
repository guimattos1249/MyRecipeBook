using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Infrastructure.DataAccess;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http.Json;

namespace WebApi.Tests;

public abstract class BaseIntegrationTest : IClassFixture<MyRecipeBookApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;

    private readonly HttpClient _client;
    internal readonly MyRecipeBookDbContext DbContext;

    public BaseIntegrationTest(MyRecipeBookApplicationFactory factory)
    {
        _client = factory.CreateClient();

        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<MyRecipeBookDbContext>();
    }

    protected async Task<HttpResponseMessage> Post(string requestUri, object request, string culture = "en")
    {
        ChangeRequestCulture(culture);

        return await _client.PostAsJsonAsync(requestUri, request);
    }

    private void ChangeRequestCulture(string culture)
    {
        _client.DefaultRequestHeaders.AcceptLanguage.Clear();
        _client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(culture);
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }
}
