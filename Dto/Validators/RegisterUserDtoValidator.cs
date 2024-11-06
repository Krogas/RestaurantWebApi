using FluentValidation;
using RestaurantWebApi.Entities;

namespace RestaurantWebApi.Dto.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.PasswordConfirm).Equal(x => x.Password);

            RuleFor(x => x.Email)
                .Custom(
                    (value, context) =>
                    {
                        var emailInUse = dbContext.Users.Any(x => x.EmailAddres == value);
                        if (emailInUse)
                        {
                            context.AddFailure("Email already in use");
                        }
                    }
                );
        }
    }
}
