namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistisActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmail(string email);
}
