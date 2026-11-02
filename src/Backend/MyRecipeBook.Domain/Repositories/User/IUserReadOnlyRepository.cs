namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistisActiveUserWithEmail(string email);
    Task<bool> ExistisActiveUserWithId(Guid userId);
    Task<Entities.User?> GetByEmail(string email);
}
