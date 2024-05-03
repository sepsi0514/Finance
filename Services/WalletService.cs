using DAO.DBModels;
using Services.Interfaces;
using UnitOfWork.Interfaces;

namespace Services
{
    public class WalletService : IWalletService<Wallet>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Wallet> _walletRepository;

        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _walletRepository = _unitOfWork.GetRepository<Wallet>();
        }

        public IQueryable<Wallet> GetWallets() => _walletRepository.GetAll();

        public void CreateWallet(Wallet wallet)
        {
            // Perform business logic and repository operations using _productRepository...

            _unitOfWork.Commit();
        }
    }
}
