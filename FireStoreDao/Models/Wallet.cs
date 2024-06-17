namespace FireStoreDao.Models
{
    public record Wallet(string uid, string name, double balance) : Base(uid, name);
}
