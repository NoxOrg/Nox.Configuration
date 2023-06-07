using Json.Schema.Generation;

namespace Nox
{

    [AdditionalProperties(false)]
    public class EtlTarget
    {
        [Required]
        [Title("The name of the etl target. Contains no spaces.")]
        [Description("The name of the etl target. It should be a commonly used singular noun and be unique within a solution.")]
        [Pattern(@"^[^\s]*$")]
        public string Name { get; internal set; } = string.Empty;

        [Title("A phrase describing the etl target.")]
        [Description("A description of the etl target.")]
        public string? Description { get; internal set; }

        [Required]
        [AdditionalProperties(false)]
        public string? DataConnection { get; internal set; }

    }
}