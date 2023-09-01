using StockCore.Entities;
using System.Threading.Tasks;

namespace StockCore.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> findAll();
        Task<Product> FindById(int id);
        Task Create(Product product);
        Task Update(int id ,Product product);
        Task Delete(Product product);
        Task<IEnumerable<Product>> Search(string name);
    }
}
