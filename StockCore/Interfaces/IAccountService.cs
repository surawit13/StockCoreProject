using StockCore.Entities;

namespace StockCore.Interfaces
{
    public interface IAccountService
    {
        Task Register(Account account);
        //Task Unregister(Account account);
        //Task Delete(Account account);
        Task<Account> Login(string username, string password);

        string GenerateToken (Account account);
        Account GetInfo(string accessToKen);
    }
}
