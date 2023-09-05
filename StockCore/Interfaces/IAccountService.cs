using StockCore.Entities;

namespace StockCore.Interfaces
{
    public interface IAccountService
    {
        Task Register(Account account);
        //Task Unregister(Account account);
        //Task Delete(Account account);
        Task Login(string username, string password);

    }
}
