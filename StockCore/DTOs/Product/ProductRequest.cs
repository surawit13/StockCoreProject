using StockCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace StockCore.DTOs.Product
{
    public class ProductRequest
    {
        //add ? mean can be null
        public int? ProductId { get; set; }
        [Required]
        [MaxLength(150,ErrorMessage ="Name, Maximun length 150")]
        public string Name { get; set; } = null!;
        [Range(0,1_000)]
        public int Stock { get; set; }
        [Range(0, 1_000_000)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        //add ? mean can be null
        public List<IFormFile>? FormFiles { get; set; }

    }
}
