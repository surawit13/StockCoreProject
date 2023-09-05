using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using StockCore.Data;
using StockCore.Entities;
using StockCore.Interfaces;
using System.Security.Cryptography;
using System.Security.Policy;

namespace StockCore.Services
{
    public class AccountService : IAccountService

    {
        private readonly DatabaseContext databaseContext;

        public AccountService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task Register(Account account)
        {
            var existingAccount = await databaseContext.Accounts.SingleOrDefaultAsync(
                a => a.Username == account.Username);
            if (existingAccount != null)
            {
                throw new Exception("Existing Account");
            }
            account.Password = CreateHashPassword(account.Password);
            databaseContext.Accounts.Add(account);
            await databaseContext.SaveChangesAsync();
        }
        public Task Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        private string CreateHashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var randomnum = RandomNumberGenerator.Create()) { 
            randomnum.GetBytes(salt);
            }
            string hased = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf:KeyDerivationPrf.HMACSHA512,
                iterationCount:10000,
                numBytesRequested:258/8
                ));


            return $"{Convert.ToBase64String(salt)}.{hased}";
        }

        private bool VerifyPassword(string hashpass , string password)
        {
            var parts = hashpass.Split('.', 2);
            if (parts.Length != 2) { 
                return false ;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var passwordHash = parts[1];

            string hased = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: password,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA512,
              iterationCount: 10000,
              numBytesRequested: 258 / 8
              ));

            return passwordHash == hased;
        }

      
    }
}
