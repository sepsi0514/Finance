using Google.Cloud.Firestore;

namespace Services.Interfaces.Models
{
    [FirestoreData]
    public class UserData
    {
        public string uid { get; set; }

        public string Email { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }        
    }
}
