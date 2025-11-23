using Application.External.Dto;
using Application.External.Dto.DamageRelations;
using Domain.PokeTypeRelations;
using Domain.PokeTypes;

namespace Application.External.Mappers
{
    public static class PokeApiMappings
    {
        private static DamageRelationsDto _damageRelationsDto = default!;
        private static ICollection<DamageRelation> _damageRelationDomainObjects = default!;

        public static PokeType ToDomain(this PokeTypeDto dto)
        {
            return PokeType.Create(dto.Id, dto.Name, dto.Sprites.Generation8.SwordShield.NameIcon);
        }

        public static ICollection<DamageRelation> ToDomains(this DamageRelationsDto dto)
        {
            _damageRelationsDto = dto;
            _damageRelationDomainObjects = new List<DamageRelation>();

            MapDoubleDamageRelations();
        }

        private static ICollection<DamageRelation> MapDoubleDamageRelations()
        {
            foreach (var damageRelationDto in _damageRelationsDto.DoubleDamageFrom)
            {

            }
        }

        //reuse for all relations in each collection
        private static void MapFromRelation()
        {

        }

        private static void MapToRelation()
        {

        }
    }
}
