using FluentValidation;

namespace Nox.Configuration.Validation.Base
{
    public class SimpleTypeValidator: AbstractValidator<NoxSimpleTypeDefinition>
    {
        public SimpleTypeValidator(string description, string objectType)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage(m => string.Format(ValidationResources.SimpleTypeNameEmpty, description, objectType));
            
            RuleFor(p => p.Type)
                .NotEmpty()
                .WithMessage(m => string.Format(ValidationResources.SimpleTypeNameEmpty, description, objectType));
        }
    }
}