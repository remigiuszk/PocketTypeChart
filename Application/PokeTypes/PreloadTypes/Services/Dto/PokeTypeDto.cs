using Application.PokeTypes.PreloadTypes.Services.Dto.DamageRelations;
using System.Text.Json.Serialization;

namespace Application.PokeTypes.PreloadTypes.Services.Dto
{
    public class PokeTypeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("damage_relations")]
        public DamageRelationsDto DamageRelations { get; set; }
    }
}
