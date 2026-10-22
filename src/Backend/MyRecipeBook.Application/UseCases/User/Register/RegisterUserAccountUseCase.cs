using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Exception.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

internal class RegisterUserAccountUseCase(IPasswordHasher passwordHasher) : IRegisterUserAccountUseCase
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public void Execute(RequestRegisterUserAccountJson request)
    {
        ValidateAndThrowFailures(request);

        var user = request.Adapt<Domain.Entities.User>();

        user.Password = _passwordHasher.HashPassword(request.Password);
    }

    private void ValidateAndThrowFailures(RequestRegisterUserAccountJson request)
    {
        var validator = new RegisterUserAccountValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
