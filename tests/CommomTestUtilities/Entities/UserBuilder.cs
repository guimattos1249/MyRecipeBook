using Bogus;
using CommomTestUtilities.Security;
using MyRecipeBook.Domain.Entities;

namespace CommomTestUtilities.Entities;

public class UserBuilder
{
    public static (User user, string password) Build()
    {
        var (password, hashedPassword) = GenerateRandomPassword();

        var user = new Faker<User>()
            .RuleFor(u => u.Name, f => f.Person.FirstName)
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(u => u.Password, _ => hashedPassword);

        return (user, password);
    }

    private static (string password, string hashedPassword) GenerateRandomPassword()
    {
        var passwordEncripter = new IPasswordHasherBuilder().Build();

        var password = new Faker().Internet.Password();

        return (password, passwordEncripter.HashPassword(password));
    }
}
