using System.Collections.Generic;
using Json.Schema.Generation;

namespace Nox;

[AdditionalProperties(false)]
public class Application
{
    [AdditionalProperties(false)]
    public List<Etl>? Etl { get; set; }

    [AdditionalProperties(false)]
    public List<DataTransferObject>? DataTransferObjects { get; set; }
}