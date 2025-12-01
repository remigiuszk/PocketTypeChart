using Application.Abstractions.Queries.Dto;

namespace Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation.PokeType
{
    public class PokeTypeReadModel
    {
        private PokeTypeReadModel(int id, string name, string sprite)
        {
            Id = id;
            Name = name;
            Sprite = sprite;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; }

        public static PokeTypeReadModel CreateFromDto(PokeTypeDto dto)
        {
            return new PokeTypeReadModel(dto.Id, dto.Name, dto.Sprite);
        }

    }
}
