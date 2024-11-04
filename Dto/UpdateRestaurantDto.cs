using System.ComponentModel.DataAnnotations;

namespace RestaurantWebApi.Dto
{
    public class UpdateRestaurantDto
    {
        [MaxLength(25)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
    }
}
