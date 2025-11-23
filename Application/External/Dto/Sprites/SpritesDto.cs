using System.Text.Json.Serialization;

namespace Application.External.Dto.Sprites
{
    public class SpritesDto
    {
        [JsonPropertyName("generation-viii")]
        public Generation8Dto Generation8 { get; set; } = default!;
    }

    public class Generation8Dto
    {
        [JsonPropertyName("sword-shield")]
        public SwordShieldDto SwordShield { get; set; } = default!;
    }

    public class SwordShieldDto
    {
        [JsonPropertyName("name_icon")]
        public string NameIcon { get; set; } = default!;
    }
}
