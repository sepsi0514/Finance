using Authentication.Models;
using FireStoreDao.Models;
using Google.Cloud.Firestore;

namespace FireStoreDao
{
    public class FinanceDatasContext
    {
        private readonly FirestoreDb _db;

        public FinanceDatasContext(FirebaseCredentials firebaseCredentials)
        {
            _db = FirestoreDb.Create(firebaseCredentials.ProjectName);
            Console.WriteLine("Created Cloud Firestore client with project ID: {0}", firebaseCredentials.ProjectName);
        }

        public CollectionReference Wallets(string email) => Users.Document(email).Collection("Wallets");
        public CollectionReference Users => _db.Collection("Users");
    }
}
