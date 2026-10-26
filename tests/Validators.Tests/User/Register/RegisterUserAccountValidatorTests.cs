using CommomTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;

namespace Validators.Tests.User.Register;

public class RegisterUserAccountValidatorTests
{
    [Fact]
    public void Success()
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();

        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}
