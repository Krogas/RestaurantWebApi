using FluentValidation;
using RestaurantWebApi.Entities;

namespace RestaurantWebApi.Dto.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginUserDtoValidator(RestaurantContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}
