using Services.Interfaces.Models;

namespace Services.Interfaces.Services
{
    public interface IWalletService
    {
        Task CreateWallet(WalletData walletData, string email);

        IAsyncEnumerable<WalletData> GetAll(string email);

        void Update(WalletData walletData);

        void Delete(string email, string id);
    }
}
