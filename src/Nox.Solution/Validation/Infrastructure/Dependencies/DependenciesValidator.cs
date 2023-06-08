using System.Collections.Generic;
using FluentValidation;

namespace Nox
{
    internal class DependenciesValidator: AbstractValidator<Dependencies>
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