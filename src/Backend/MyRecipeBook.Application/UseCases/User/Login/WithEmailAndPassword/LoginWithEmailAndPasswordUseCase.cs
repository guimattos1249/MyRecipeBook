using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.PasswordHashing;

namespace MyRecipeBook.Application.UseCases.User.Login.WithEmailAndPassword;

public class LoginWithEmailAndPasswordUseCase(IPasswordHasher passwordHashser, IUserReadOnlyRepository userReadOnlyRepository) : ILoginWithEmailAndPasswordUseCase
{
    private readonly IPasswordHasher _passwordHashser = passwordHashser;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await _userReadOnlyRepository.GetByEmail(request.Email);

        var isPasswordValid = _passwordHashser.VerifyPassword(request.Password, user.Password);

        return new ResponseRegisteredUserJson(user.Name, new ResponseTokensJson(string.Empty, string.Empty));
    }
}
