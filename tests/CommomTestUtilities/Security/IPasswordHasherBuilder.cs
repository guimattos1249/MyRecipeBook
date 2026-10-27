using Moq;
using MyRecipeBook.Domain.Security.PasswordHashing;

namespace CommomTestUtilities.Security;

public class IPasswordHasherBuilder
{
    private readonly Mock<IPasswordHasher> _mock;
    public IPasswordHasherBuilder()
    {
        _mock = new Mock<IPasswordHasher>();

        _mock.Setup(hasher => hasher.HashPassword(It.IsAny<string>())).Returns("hashed_password");
    }

    public void VerifyPassword(string password)
    {
        _mock.Setup(repository => repository.VerifyPassword(password, It.IsAny<string>())).Returns(true);
    }

    public IPasswordHasher Build() => _mock.Object;
}
