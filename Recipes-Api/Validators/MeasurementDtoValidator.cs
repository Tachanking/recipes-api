using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class MeasurementDtoValidator : AbstractValidator<MeasurementDto>
    {
        public MeasurementDtoValidator()
        {
            RuleFor(measurement => measurement.Id).NotNull();
            RuleFor(measurement => measurement.Id).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(measurement => measurement.Name).NotEmpty();
            RuleFor(measurement => measurement.Name).MaximumLength(Constants.NameMaximumLength);
        }
    }
}
