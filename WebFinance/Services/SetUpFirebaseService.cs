using Authentication.Interfaces;
using Authentication.Models;
using System.Text.Json;

namespace WebFinance.Services
{
    public class SetUpFirebaseService : ICredentialsService
    {
        public FirebaseCredentials LoadFireBaseCredentials(string path)
             => JsonSerializer.Deserialize<FirebaseCredentials>(File.ReadAllText(path));

    }
}
