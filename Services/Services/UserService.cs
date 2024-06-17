using FireStoreDao;
using Google.Cloud.Firestore;
using Services.Interfaces.Models;
using Services.Interfaces.Services;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly FinanceDatasContext _financeDatasContext;

        public UserService(FinanceDatasContext financeDatasContext)
        {
            _financeDatasContext = financeDatasContext;
        }

        public async Task CreateUser(UserData userData) => await _financeDatasContext.Users.Document(userData.Email).SetAsync(userData);

        public async Task<UserData> GetUser(string email)
        {
            var snapshot =  _financeDatasContext.Users.Document(email);
            return await Task.Run(() => new UserData());
        }
    }
}
