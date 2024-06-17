using Services.Interfaces.Models;

namespace Services.Interfaces
{
    public interface IWalletService
    {
        void CreateWallet(WalletData walletData);

        IAsyncEnumerable<WalletData> GetAll();

        void Update(WalletData walletData);

        void Delete(string id);
    }
}
