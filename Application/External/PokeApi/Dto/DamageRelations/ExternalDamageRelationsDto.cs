using System.Text.Json.Serialization;

namespace Application.External.PokeApi.Dto.DamageRelations
{
    public class ExternalDamageRelationsDto
    {
        [JsonPropertyName("no_damage_to")]
        public List<ExternalDamageRelationDto> NoDamageTo { get; set; }

        [JsonPropertyName("no_damage_from")]
        public List<ExternalDamageRelationDto> NoDamageFrom { get; set; }

        [JsonPropertyName("half_damage_to")]
        public List<ExternalDamageRelationDto> HalfDamageTo { get; set; }

        [JsonPropertyName("half_damage_from")]
        public List<ExternalDamageRelationDto> HalfDamageFrom { get; set; }

        [JsonPropertyName("double_damage_to")]
        public List<ExternalDamageRelationDto> DoubleDamageTo { get; set; }

        [JsonPropertyName("double_damage_from")]
        public List<ExternalDamageRelationDto> DoubleDamageFrom { get; set; }
    }
}
