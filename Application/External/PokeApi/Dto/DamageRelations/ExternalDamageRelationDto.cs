using System.Text.Json.Serialization;

namespace Application.External.PokeApi.Dto.DamageRelations
{
    public class ExternalDamageRelationDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("url")]
        public string Url { get; set; } = default!;
    }
}
