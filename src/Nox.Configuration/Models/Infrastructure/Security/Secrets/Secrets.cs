using Json.Schema.Generation;

namespace Nox;

[AdditionalProperties(false)]
public class Secrets
{
    [AdditionalProperties(false)]
    public SecretsServer? SecretsServer { get; set; }
    
    [AdditionalProperties(false)]
    public SecretsValidFor? ValidFor { get; set; }
}