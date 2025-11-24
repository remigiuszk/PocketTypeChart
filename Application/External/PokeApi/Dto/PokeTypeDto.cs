using Application.External.PokeApi.Dto.DamageRelations;
using Application.External.PokeApi.Dto.Sprites;
using Domain.PokeTypes;
using System.Text.Json.Serialization;

namespace Application.External.PokeApi.Dto
{
    public class PokeTypeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("damage_relations")]
        public DamageRelationsDto DamageRelations { get; set; } = default!;

        [JsonPropertyName("sprites")]
        public SpritesDto Sprites { get; set; } = default!;
    }
}
