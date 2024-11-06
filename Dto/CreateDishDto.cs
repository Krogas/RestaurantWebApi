using System.ComponentModel.DataAnnotations;

namespace RestaurantWebApi.Dto
{
    public class CreateDishDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
