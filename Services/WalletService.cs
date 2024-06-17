using FireStoreDao;
using Google.Cloud.Firestore;
using Services.Interfaces;
using Services.Interfaces.Models;

namespace Services
{
    public class WalletService : IWalletService
    {
        private readonly FinanceDatasContext _financeDatasContext;

        public WalletService(FinanceDatasContext financeDatasContext)
        {
            _financeDatasContext = financeDatasContext;
        }

        public async void CreateWallet(WalletData walletData) => await _financeDatasContext.Wallets.AddAsync(walletData);

        public async void Delete(string id) => await _financeDatasContext.Wallets.Document(id).DeleteAsync();

        public async IAsyncEnumerable<WalletData> GetAll()
        {
            QuerySnapshot snapshot = await _financeDatasContext.Wallets.GetSnapshotAsync();

            foreach (DocumentSnapshot document in snapshot.Documents)
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
