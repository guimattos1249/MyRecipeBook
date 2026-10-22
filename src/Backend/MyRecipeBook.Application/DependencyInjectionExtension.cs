using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Domain.Security.PasswordHashing;


namespace MyRecipeBook.Application;

public class DependencyInjectionExtension
{
    public static void AddApplication(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserAccountUseCase, RegisterUserAccountUseCase>();
    }
}
