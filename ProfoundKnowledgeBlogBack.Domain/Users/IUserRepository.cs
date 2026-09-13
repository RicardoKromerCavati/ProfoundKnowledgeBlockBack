namespace ProfoundKnowledgeBlogBack.Domain.Users;

public interface IUserRepository
{
    ValueTask InsertUser(User user);
    ValueTask<int> SelectCountByEmail(string email);
    ValueTask<int> SelectCountByUserId(Guid userId);
    ValueTask<int> SelectCountByUsername(string username);
    ValueTask<User?> SelectDbUserByEmail(string email);
}