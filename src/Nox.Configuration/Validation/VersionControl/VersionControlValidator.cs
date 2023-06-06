using FluentValidation;

namespace Nox.Configuration.Validation
{
    public class VersionControlValidator: AbstractValidator<VersionControl>
    {
        public VersionControlValidator()
        {
            RuleFor(vc => vc.Provider)
                .NotEmpty()
                .WithMessage(vc => string.Format(ValidationResources.VersionControlProviderEmpty));

            RuleFor(vc => vc.Host)
                .NotEmpty()
                .WithMessage(vc => string.Format(ValidationResources.VersionControlHostEmpty));

            RuleFor(vc => vc.Folders!)
                .SetValidator(vc => new VersionControlFoldersValidator());
        }
    }
}