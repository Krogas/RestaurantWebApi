namespace RestaurantWebApi.Dto
{
    public class RestaurantDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool HasDelivery { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string? PostalCode { get; set; }
        public virtual List<DishDto> Dishes { get; set; }
    }
}
