using FireStoreDao;
using Google.Cloud.Firestore;
using Services.Interfaces.Models;
using Services.Interfaces.Services;

namespace Services.Services
{
    public class WalletService : IWalletService
    {
        private readonly FinanceDatasContext _financeDatasContext;

        public WalletService(FinanceDatasContext financeDatasContext)
        {
            _financeDatasContext = financeDatasContext;
        }

        public async Task CreateWallet(WalletData walletData, string email)
        {
            await _financeDatasContext.Users.Document(email).Collection("Wallets").AddAsync(walletData);
        }            

        public async void Delete(string email, string id) =>
            await _financeDatasContext.Wallets(email).Document(id).DeleteAsync();

        public async IAsyncEnumerable<WalletData> GetAll(string email)
        {
            var walletsSnap = await _financeDatasContext.Wallets(email).GetSnapshotAsync();

            foreach (DocumentSnapshot document in walletsSnap.Documents)
            {
                WalletData wd = document.ConvertTo<WalletData>();
                wd.uid = document.Id;
                yield return wd;
            }
        }

        public void Update(WalletData walletData)
        {
            throw new NotImplementedException();
        }
    }
}
