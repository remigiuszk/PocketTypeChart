using System.Text.Json.Serialization;

namespace Application.External.Dto.DamageRelations
{
    public class DamageRelationDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
