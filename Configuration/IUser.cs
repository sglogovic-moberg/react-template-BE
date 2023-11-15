using ReactAppBackend.Models;

namespace ReactAppBackend.Configuration;

public interface IUser
{
    public int UserId { get; }

    public string Username { get; }

    public UserRoleEnum UserRole { get; }

}
