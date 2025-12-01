using Domain.PokeTypes;
using System.Runtime.CompilerServices;

namespace Application.Abstractions.Queries.Dto
{
    public class PokeTypeDto
    {
        private PokeTypeDto(int id, string name, string sprite)
        {
            Id = id;
            Name = name;
            Sprite = sprite;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; }

        public static PokeTypeDto CreateFromDomain(PokeType pokeTypeDomain)
        {
            return new PokeTypeDto(pokeTypeDomain.Id, pokeTypeDomain.Name!, pokeTypeDomain.Sprite!);
        }
    }
}
