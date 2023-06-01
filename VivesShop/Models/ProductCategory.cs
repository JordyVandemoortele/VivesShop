using System.ComponentModel.DataAnnotations;

namespace VivesShop.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Display(Name = "Naam categorie:")]
        [Required]
        public required string Name { get; set; }
        public IList<Product> AssignedProducts { get; set; } = new List<Product>();
    }
}
