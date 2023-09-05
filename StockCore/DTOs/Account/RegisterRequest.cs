using System.ComponentModel.DataAnnotations;

namespace StockCore.DTOs.Account
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string username { get; set; }

        [Required]
        [MinLength(8)]
        public string password { get; set; }
        public int roleid { get; set; }

    }
}
