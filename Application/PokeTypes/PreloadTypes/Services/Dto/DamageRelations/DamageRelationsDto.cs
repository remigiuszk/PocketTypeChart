using System.Text.Json.Serialization;

namespace Application.PokeTypes.PreloadTypes.Services.Dto.DamageRelations
{
    public class DamageRelationsDto
    {
        [JsonPropertyName("no_damage_to")]
        public List<DamageRelationDto> NoDamageTo { get; set; }

        [JsonPropertyName("no_damage_from")]
        public List<DamageRelationDto> NoDamageFrom { get; set; }

        [JsonPropertyName("half_damage_to")]
        public List<DamageRelationDto> HalfDamageTo { get; set; }

        [JsonPropertyName("half_damage_from")]
        public List<DamageRelationDto> HalfDamageFrom { get; set; }

        [JsonPropertyName("double_damage_to")]
        public List<DamageRelationDto> DoubleDamageTo { get; set; }

        [JsonPropertyName("double_damage_from")]
        public List<DamageRelationDto> DoubleDamageFrom { get; set; }
    }
}
