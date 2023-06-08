using System.Collections.Generic;
using FluentValidation;

namespace Nox.Validation.Infrastructure.Dependencies
{
    public class NotificationsValidator: AbstractValidator<Notifications>
    {
        public NotificationsValidator(IEnumerable<ServerBase>? servers)
        {
            RuleFor(p => p.EmailServer!)
                .SetValidator(v => new EmailServerValidator(servers));
            
            RuleFor(p => p.SmsServer!)
                .SetValidator(v => new SmsServerValidator(servers));
            
            RuleFor(p => p.ImServer!)
                .SetValidator(v => new ImServerValidator(servers));
        }
    }
}