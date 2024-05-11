﻿using Firebase.Auth;

namespace WebFinance.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private readonly FirebaseAuthClient _firebaseAuth;

        public FirebaseAuthService(FirebaseAuthClient firebaseAuth)
        {
            _firebaseAuth = firebaseAuth;
        }

        public async Task<string?> SignUp(string email, string password)
        {
            var userCredentials = await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password);

            return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();
        }

        public async Task<string?> Login(string email, string password)
        {
            try
            {
                var userCredentials = await _firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);

                return userCredentials is null ? null : await userCredentials.User.GetIdTokenAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void SignOut() => _firebaseAuth.SignOut();
    }
}
