using Bogus;
using CommomTestUtilities.Security;
using MyRecipeBook.Domain.Entities;

namespace CommomTestUtilities.Entities;

public class UserBuilder
{
    public static User Build()
    {
        return new Faker<User>()
            .RuleFor(u => u.Name, f => f.Person.FirstName)
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(u => u.Password, _ => GenerateRandomPassword());
    }

    private static string GenerateRandomPassword()
    {
        var passwordEncripter = new IPasswordHasherBuilder().Build();

        var password = new Faker().Internet.Password();

        return passwordEncripter.HashPassword(password);
    }
}
