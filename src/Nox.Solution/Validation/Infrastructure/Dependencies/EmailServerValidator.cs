using System.Collections.Generic;
using FluentValidation;
using Nox.Solution.Extensions;

namespace Nox.Solution.Validation
{
    public class EmailServerValidator: AbstractValidator<EmailServer>
    {
        public EmailServerValidator(IEnumerable<ServerBase>? servers)
        {
            Include(new ServerBaseValidator("the infrastructure, dependencies, notifications, email server", servers));
            RuleFor(p => p.Provider)
                .NotEmpty()
                .WithMessage(p => string.Format(ValidationResources.EmailServerProviderEmpty, p.Name, EmailServerProvider.SendGrid.ToNameList()));
        }
    }
}