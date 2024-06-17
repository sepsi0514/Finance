using Authentication.Models;
using Google.Cloud.Firestore;

namespace FireStoreDao
{
    public class DbBuilder
    {
        public DbBuilder(FirebaseCredentials firebaseCredentials)
        {
            FirestoreDb db = FirestoreDb.Create(firebaseCredentials.ProjectName);
            Console.WriteLine("Created Cloud Firestore client with project ID: {0}", firebaseCredentials.ProjectName);

            CollectionReference wallets = db.Collection("Wallets");
            Wallets(wallets);
        }

        public async void Wallets(CollectionReference collectionReference)
        {
            QuerySnapshot snapshot = await collectionReference.GetSnapshotAsync();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Console.WriteLine("User: {0}", document.Id);
                Dictionary<string, object> documentDictionary = document.ToDictionary();               
            }
        }
    }
}
