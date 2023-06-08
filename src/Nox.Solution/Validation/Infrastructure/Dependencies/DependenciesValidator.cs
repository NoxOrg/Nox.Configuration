using System.Collections.Generic;
using FluentValidation;
using Nox.Validation.Base;

namespace Nox.Validation.Infrastructure.Dependencies
{
    public class DependenciesValidator: AbstractValidator<Nox.Dependencies>
    {
        public DependenciesValidator(IEnumerable<ServerBase>? servers)
        {
            RuleFor(p => p.Notifications!)
                .SetValidator(v => new NotificationsValidator(servers));
            
            RuleFor(p => p.Monitoring!)
                .SetValidator(v => new ServerBaseValidator("the infrastructure, dependencies, monitoring server", servers));
            
            RuleFor(p => p.Translations!)
                .SetValidator(v => new ServerBaseValidator("the infrastructure, dependencies, translations server", servers));

            RuleFor(p => p.Security!)
                .SetValidator(v => new SecurityValidator(servers));

            RuleForEach(p => p.DataConnections)
                .SetValidator(v => new DataConnectionValidator(servers));
        }
    }
}