using Google.Cloud.Firestore;

namespace Services.Interfaces.Models
{
    [FirestoreData]
    public class WalletData
    {
        public string uid { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public double Balance { get; set; }
        [FirestoreProperty]
        public string Color { get; set; }
        [FirestoreProperty]
        public bool IsCash { get; set; }

        public WalletData()
        {
            
        }

        public WalletData(string uid, string Name, double Balance, string Color, bool IsCash)
        {
            this.uid = uid;
            this.Name = Name;
            this.Balance = Balance;
            this.Color = Color;
            this.IsCash = IsCash;
        }
    }
}
