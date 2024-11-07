using FluentValidation;
using RestaurantWebApi.Entities;

namespace RestaurantWebApi.Dto.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private readonly string[] allowedSortByColumnNames =
        {
            nameof(Restaurant.Name),
            nameof(Restaurant.Category),
            nameof(Restaurant.Description),
        };

        public RestaurantQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);

            RuleFor(r => r.SortBy)
                .Must(
                    value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value)
                )
                .WithMessage(
                    $"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]"
                );
        }
    }
}
