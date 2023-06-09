using System.Collections.Generic;
using FluentValidation;
using Nox.Solution.Extensions;

namespace Nox.Solution.Validation;

public class DatabaseServerValidator: AbstractValidator<DatabaseServer>
{
    public DatabaseServerValidator(IEnumerable<ServerBase>? servers)
    {
        Include(new ServerBaseValidator("the infrastructure, persistence, database server", servers));
        RuleFor(p => p.Provider)
            .NotEmpty()
            .WithMessage(p => string.Format(ValidationResources.DatabaseServerProviderEmpty, p.Name, DatabaseServerProvider.SqlServer.ToNameList()));
    }
}