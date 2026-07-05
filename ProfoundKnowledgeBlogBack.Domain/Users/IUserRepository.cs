namespace ProfoundKnowledgeBlogBack.Domain.Users;

public interface IUserRepository
{
    ValueTask InsertUser(IUser user);
    ValueTask<int> SelectCountByEmail(string email);
    ValueTask<int> SelectCountByUserId(Guid userId);
    ValueTask<int> SelectCountByUsername(string username);
    ValueTask<IUser?> SelectDbUserByEmail(string email);
}