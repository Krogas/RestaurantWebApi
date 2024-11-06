namespace RestaurantWebApi.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string EmailAddres { get; set; } = null!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
