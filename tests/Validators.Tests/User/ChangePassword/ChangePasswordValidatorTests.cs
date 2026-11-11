using CommomTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Exception;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Validators.Tests.User.ChangePassword;

public class ChangePasswordValidatorTests
{
    [Fact]
    public void Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build();

        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "Intentional becuse is a unit test")]
    public void Validate_ShouldHaveError_WhenPasswordIsEmpty(string password)
    {
        var request = RequestChangePasswordJsonBuilder.Build() with
        {
            NewPassword = password
        };

        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();

        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_PASSWORD_REQUIRED));
        });
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    
    [SuppressMessage("Usage", "xUnit1012:Null should only be used for nullable parameters", Justification = "Intentional becuse is a unit test")]
    public void Validate_ShouldHaveError_WhenPasswordIsTooShort(string password)
    {
        var request = RequestChangePasswordJsonBuilder.Build() with
        {
            NewPassword = password
        };

        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();

        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_PASSWORD_MIN_LENGTH));
        });
    }
}
