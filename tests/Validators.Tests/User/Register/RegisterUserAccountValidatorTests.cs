using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;

namespace Validators.Tests.User.Register;

public class RegisterUserAccountValidatorTests
{
    [Fact]
    public void Success()
    {
        var request = new RequestRegisterUserAccountJson("John Doe", "johndoe@gmail.com", "123456789");

        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}
