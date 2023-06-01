using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace VivesShop.Models
{
    public class ProductCart
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public int NumberAdded { get; set; }
    }
}
