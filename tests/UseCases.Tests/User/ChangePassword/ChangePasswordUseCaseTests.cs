using CommomTestUtilities.Entities;
using CommomTestUtilities.Identity;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using CommomTestUtilities.Security;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;
using Shouldly;

namespace UseCases.Tests.User.ChangePassword;

public class ChangePasswordUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build() with
        {
            CurrentPassword = password
        };
        

        var useCase = CreateUseCase(user, password);

        await useCase.Execute(request).ShouldNotThrowAsync();
    }

    [Fact]
    public async Task Validate_ShouldThrowException_WhenPasswordIsEmpty()
    {
        (var user, var password) = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build() with
        {
            CurrentPassword = password,
            NewPassword = string.Empty
        };

        var useCase = CreateUseCase(user, password);

        var ex = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        ex.GetErrorMessages().ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(ResourceMessagesException.VALIDATION_PASSWORD_REQUIRED);
        });
    }

    [Fact]
    public async Task Validate_ShouldThrowException_WhenCurrentPasswordDoesNotMatch()
    {
        (var user, var password) = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user, "invalid-password");

        var ex = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        ex.GetErrorMessages().ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(ResourceMessagesException.VALIDATION_CURRENT_PASSWORD_INCORRECT);
        });
    }

    private static ChangePasswordUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user, string password)
    {
        var loggedUser = ILoggedUserBuilder.Build(user);
        var hasher = new IPasswordHasherBuilder().VerifyPassword(password).Build();
        var userUpdateOnlyRepository = IUserUpdateOnlyRepositoryBuilder.Build();

        return new ChangePasswordUseCase(loggedUser, hasher, userUpdateOnlyRepository);
    }
}
