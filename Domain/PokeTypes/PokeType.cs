using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.PokeTypes
{
    public class PokeType
    {
        private PokeType(int id, string? name, string? sprite)
        {
            Id = id;
            Name = name;
            Sprite = sprite;
        }

        protected PokeType()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Sprite { get; set; }
        public byte[]? SpriteImage { get; set; }

        public static PokeType Create(int id, string? name, string? sprite)
        {
            return new PokeType(id, name, sprite);
        }
    }
}
