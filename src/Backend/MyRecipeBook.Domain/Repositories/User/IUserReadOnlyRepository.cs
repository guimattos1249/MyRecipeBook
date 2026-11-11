namespace MyRecipeBook.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistsActiveUserWithEmail(string email);
    Task<bool> ExistsActiveUserWithId(Guid userId);
    Task<Entities.User?> GetByEmail(string email);
}
