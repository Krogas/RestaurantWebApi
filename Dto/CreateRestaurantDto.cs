using System.ComponentModel.DataAnnotations;

namespace RestaurantWebApi.Dto
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool HasDelivery { get; set; }

        [EmailAddress]
        public string? ContactEmail { get; set; }

        [Phone]
        public string? ContactNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Street { get; set; } = null!;
        public string? PostalCode { get; set; }
    }
}
