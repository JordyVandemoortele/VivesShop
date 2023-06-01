using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace VivesShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Naam van product:")]
        [Required]
        public required string Name { get; set; }
        [Display(Name = "Prijs van product:")]
        [Range(0, double.MaxValue, ErrorMessage = "De prijs kan niet lager dan €0 zijn")]
        [Required]
        public required decimal Price { get; set; }

        public int? CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
    }
}
