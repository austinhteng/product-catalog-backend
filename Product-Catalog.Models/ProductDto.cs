using System.ComponentModel.DataAnnotations;

namespace Product_Catalog.Models
{
    public class ProductDto
    {
        //[Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        //[ProductIdNotUsed]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(2, ErrorMessage = "Minimum 2 characters")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be at least 0.01")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
    }
}
