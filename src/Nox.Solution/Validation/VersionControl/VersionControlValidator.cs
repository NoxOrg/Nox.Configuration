using FluentValidation;

namespace Nox.Validation.VersionControl
{
    public class VersionControlValidator: AbstractValidator<Nox.VersionControl>
    {
        public VersionControlValidator()
        {
            RuleFor(vc => vc.Provider)
                .NotNull()
                .WithMessage(vc => string.Format(ValidationResources.VersionControlProviderEmpty));

            RuleFor(vc => vc.Host)
                .NotEmpty()
                .WithMessage(vc => string.Format(ValidationResources.VersionControlHostEmpty));

            RuleFor(vc => vc.Folders!)
                .SetValidator(vc => new VersionControlFoldersValidator());
        }
    }
}