using FluentValidation;

namespace Nox.Solution.Validation
{
    public class EtlLookupValidator: AbstractValidator<IntegrationLookup>
    {
        public EtlLookupValidator(string etlName)
        {
            RuleFor(p => p.SourceColumn)
                .NotEmpty()
                .WithMessage(string.Format(ValidationResources.EtlLookupSourceColumnEmpty, etlName));

            RuleFor(p => p.Match)
                .NotEmpty()
                .WithMessage(string.Format(ValidationResources.EtlLookupMatchEmpty, etlName));

            RuleFor(p => p.Match!)
                .SetValidator(new EtlMatchValidator(etlName));
            
            RuleFor(p => p.TargetAttribute)
                .NotEmpty()
                .WithMessage(string.Format(ValidationResources.EtlLookupTargetAttributeEmpty, etlName));
        }
    }
}