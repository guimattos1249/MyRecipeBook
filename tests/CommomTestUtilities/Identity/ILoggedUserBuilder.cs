using Bogus;
using Moq;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Identity;

namespace CommomTestUtilities.Identity;

public class ILoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var moq = new Mock<ILoggedUser>();

        moq.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        moq.Setup(loggedUser => loggedUser.GetUserId()).Returns(user.Id);

        return moq.Object;
    }
}
