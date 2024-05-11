using Authentication.Models;

namespace Authentication.Interfaces
{
    public interface ICredentialsService
    {
        FirebaseCredentials LoadFireBaseCredentials(string path);
    }
}
