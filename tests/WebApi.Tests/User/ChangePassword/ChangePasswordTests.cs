using CommomTestUtilities.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exception;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;
using WebApi.Tests.Resources;

namespace WebApi.Tests.User.ChangePassword;

public class ChangePasswordTests(MyRecipeBookApplicationFactory factory) : BaseIntegrationTest(factory)
{
    private const string REQUEST_URI = "users/change-password";
    private readonly UserIdentityManager _user1 = factory.User1;

    [Fact]
    public async Task Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build() with
        {
            CurrentPassword = _user1.GetPassword(),
        };

        var response = await Patch(REQUEST_URI, request, _user1.GetAccessToken());

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Validate_ShouldBeAnErrorResponse_WhenNewPasswordIsEmpty(string culture)
    {
        var request = RequestChangePasswordJsonBuilder.Build() with
        {
            NewPassword = string.Empty
        };

        var response = await Patch(REQUEST_URI, request, _user1.GetAccessToken(), culture);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedErrorMessage = ResourceMessagesException.ResourceManager.GetString("VALIDATION_PASSWORD_REQUIRED", new CultureInfo(culture));

        errors.ShouldSatisfyAllConditions(errorsList =>
        {
            errorsList.Count().ShouldBe(2);
            errorsList.ShouldContain(error => error.GetString().IsNotEmpty() && error.GetString()!.Equals(expectedErrorMessage));
        });
    }
}
