using CommomTestUtilities.Entities;
using CommomTestUtilities.Identity;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;
using Shouldly;

namespace UseCases.Tests.User.Update;

public class UpdateUserUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        (var user, var _) = UserBuilder.Build();
        
        var request = RequestUpdateUserJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        await useCase.Execute(request).ShouldNotThrowAsync();

        user.Name.ShouldBe(request.Name);
        user.Email.ShouldBe(request.Email);
    }

    [Fact]
    public async Task Validate_ShouldThrowException_WhenNameIsEmpty()
    {
        (var user, var _) = UserBuilder.Build();

        var request = RequestUpdateUserJsonBuilder.Build() with
        {
            Name = string.Empty
        };

        var useCase = CreateUseCase(user);

        var ex = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        ex.GetErrorMessages().ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(ResourceMessagesException.VALIDATION_NAME_REQUIRED);
        });

        user.Name.ShouldNotBe(request.Name);
        user.Email.ShouldNotBe(request.Email);
    }

    [Fact]
    public async Task Validate_ShouldThrowException_WhenEmailAlreadyExists()
    {
        (var user, var _) = UserBuilder.Build();

        var request = RequestUpdateUserJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Email);

        var ex = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        ex.GetErrorMessages().ShouldSatisfyAllConditions(errorMessages =>
        {
            errorMessages.Count.ShouldBe(1);
            errorMessages.ShouldContain(ResourceMessagesException.VALIDATION_EMAIL_ALREADY_EXISTS);
        });
    }


    private static UpdateUserUseCase CreateUseCase(
        MyRecipeBook.Domain.Entities.User user, 
        string? emailThatAlreadyExists = null)
    {
        var loggedUser = ILoggedUserBuilder.Build(user);
        var userUpdateOnlyRepository = IUserUpdateOnlyRepositoryBuilder.Build();
        var unitOfWork = IUnitOfWorkBuilder.Build();

        var userReadOnlyRepositoryBuilder = new IUserReadOnlyRepositoryBuilder();
        if (emailThatAlreadyExists.IsNotEmpty())
            userReadOnlyRepositoryBuilder.ExistisActiveUserWithEmail(emailThatAlreadyExists);

        return new UpdateUserUseCase(loggedUser, userReadOnlyRepositoryBuilder.Build(), userUpdateOnlyRepository, unitOfWork);
    }
}
