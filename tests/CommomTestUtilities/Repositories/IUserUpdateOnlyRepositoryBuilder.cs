using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommomTestUtilities.Repositories;

public class IUserUpdateOnlyRepositoryBuilder
{
    public static IUserUpdateOnlyRepository Build()
    {
        var moq = new Mock<IUserUpdateOnlyRepository>();

        return moq.Object;
    }
}
