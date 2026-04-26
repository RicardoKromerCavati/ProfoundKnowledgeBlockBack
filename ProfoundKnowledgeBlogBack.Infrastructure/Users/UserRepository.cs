using Microsoft.EntityFrameworkCore;
using ProfoundKnowledgeBlogBack.Domain.Users;

namespace ProfoundKnowledgeBlogBack.Infrastructure.Users;

public class UserRepository(ProfoundKnowledgeContext profoundKnowledgeContext) : IUserRepository
{
    public async ValueTask<int> SelectCountByEmail(string email)
    {
        email = email.ToLower();
        return await profoundKnowledgeContext.Users.CountAsync(u => u.Email.Equals(email));
    }

    public async ValueTask<int> SelectCountByUsername(string username)
    {
        username = username.ToLower();
        return await profoundKnowledgeContext.Users.CountAsync(u => u.Username.Equals(username));
    }

    public async ValueTask InsertUser(IUser user)
    {
        var dbUser = DbUser.Create(user);
        await profoundKnowledgeContext.Users.AddAsync(dbUser);
        await profoundKnowledgeContext.SaveChangesAsync();
    }

    public async ValueTask<IUser?> SelectDbUserByEmail(string email)
    {
        email = email.ToLower();
        return await profoundKnowledgeContext.Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
    }
}