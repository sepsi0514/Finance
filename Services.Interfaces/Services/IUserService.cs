using Services.Interfaces.Models;

namespace Services.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUser(UserData userData);

        Task<UserData> GetUser(string uid);
    }
}
