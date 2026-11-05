using Mapster;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Identity;

namespace MyRecipeBook.Application.UseCases.User.Profile;

public class GetUserProfileUseCase(ILoggedUser loggedUser) : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseUserProfileJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        return new ResponseUserProfileJson(loggedUser.Name, loggedUser.Email);
    }
}
