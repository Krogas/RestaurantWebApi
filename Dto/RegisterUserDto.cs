namespace RestaurantWebApi.Dto
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Password { get; set; } = null!;

        public int RoleId { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
