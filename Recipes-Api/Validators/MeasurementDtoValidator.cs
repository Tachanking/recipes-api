using FluentValidation;
using Recipes_Api.Dto;
using Recipes_Api.Utility;

namespace Recipes_Api.Validators
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
