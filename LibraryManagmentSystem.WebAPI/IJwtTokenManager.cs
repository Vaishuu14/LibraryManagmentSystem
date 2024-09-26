using LibraryManagmentSystem.Domain.Entities;

namespace LibraryManagmentSystem.WebAPI
{
    public interface IJwtTokenManager
    {
        string IssueToken(User user);
    }
}
