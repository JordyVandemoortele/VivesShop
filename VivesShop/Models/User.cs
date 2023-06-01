using System.ComponentModel.DataAnnotations;

namespace VivesShop.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        public List<Product> ShoppingCart { get; set; } = new List<Product>();
        public decimal TotalPrice { get; set; }

        public void GetTotal()
        {
            TotalPrice = 0;
            foreach (var item in ShoppingCart)
            {
                TotalPrice += item.Price;
            }
        }
    }
}
