namespace FireStoreDao.Models
{
    public record User(string uid, string name, IEnumerable<Wallet> wallets) : Base(uid, name);
}
