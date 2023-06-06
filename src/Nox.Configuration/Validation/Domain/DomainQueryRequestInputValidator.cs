using FluentValidation;

namespace Nox.Configuration.Validation
{
    public class DomainQueryRequestInputValidator: AbstractValidator<DomainQueryRequestInput>
    {
        public DomainQueryRequestInputValidator(string queryName)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(m => string.Format(ValidationResources.DomainQueryRequestNameEmpty, queryName));
            
            RuleFor(p => p.Type)
                .NotEmpty()
                .WithMessage(m => string.Format(ValidationResources.DomainQueryRequestTypeEmpty, queryName));
        }
    }
}