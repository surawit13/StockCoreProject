using StockCore.Entities;
namespace StockCore.DTOs.Product
{
    public class ProductResponse
    {
        public int? ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

        public static ProductResponse FromProduct(StockCore.Entities.Product product){

            return new ProductResponse
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                Stock = product.Stock,
                CategoryName = product.Category.Name,

            };
        }
    }
}
