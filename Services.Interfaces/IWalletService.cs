namespace Services.Interfaces
{
    public interface IWalletService<Type>
    {
        IQueryable<Type> GetWallets();
    }
}
