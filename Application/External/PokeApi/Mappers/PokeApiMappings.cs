using Application.External.PokeApi.Dto;
using Application.External.PokeApi.Dto.DamageRelations;
using Domain.PokeTypeRelations;
using Domain.PokeTypes;

namespace Application.External.PokeApi.Mappers
{
    public static class PokeApiMappings
    {
        private static DamageRelationsDto _damageRelationsDto = default!;
        private static ICollection<DamageRelation> _damageRelationDomainObjects = default!;
        private static int _pokeTypeId;

        public static PokeType ToDomain(this PokeTypeDto dto)
        {
            return PokeType.Create(dto.Id, dto.Name, dto.Sprites.Generation8.SwordShield.NameIcon);
        }

        public static ICollection<DamageRelation> MapDamageRelations(this PokeTypeDto dto)
        {
            _pokeTypeId = dto.Id;
            _damageRelationsDto = dto.DamageRelations;
            _damageRelationDomainObjects = new List<DamageRelation>();

            MapRecievingRelations();
            MapOutgoingRelations();

            return _damageRelationDomainObjects;
        }

        private static void MapRecievingRelations()
        {
            MapRecievingRelation(_damageRelationsDto.DoubleDamageFrom, 2);
            MapRecievingRelation(_damageRelationsDto.HalfDamageFrom, 0.5);
            MapRecievingRelation(_damageRelationsDto.NoDamageFrom, 0);
        }

        private static void MapRecievingRelation(ICollection<DamageRelationDto> relations, double multiplier)
        {
            foreach (var damageRelationDto in relations)
            {
                var damageRelationDomainObject = damageRelationDto.RecievingRelationToDomain(multiplier);
                _damageRelationDomainObjects.Add(damageRelationDomainObject);
            }
        }

        private static DamageRelation RecievingRelationToDomain(this DamageRelationDto dto, double multiplier)
        {
            return DamageRelation.Create(ExtractAndConvertIdFromPath(dto.Url), _pokeTypeId, multiplier); 
        }

        private static int ExtractAndConvertIdFromPath(string url)
        {
            var idFromUrl = url.Trim('/').Split('/').LastOrDefault();
            return Convert.ToInt32(idFromUrl);
        }

        private static void MapOutgoingRelations()
        {
            MapOutgoingRelation(_damageRelationsDto.DoubleDamageFrom, 2);
            MapOutgoingRelation(_damageRelationsDto.HalfDamageFrom, 0.5);
            MapOutgoingRelation(_damageRelationsDto.NoDamageFrom, 0);
        }

        private static void MapOutgoingRelation(ICollection<DamageRelationDto> relations, double multiplier)
        {
            foreach (var damageRelationDto in relations)
            {
                var damageRelationDomainObject = damageRelationDto.OutgoingRelationToDomain(multiplier);
                _damageRelationDomainObjects.Add(damageRelationDomainObject);
            }
        }

        private static DamageRelation OutgoingRelationToDomain(this DamageRelationDto dto, double multiplier)
        {
            return DamageRelation.Create(_pokeTypeId, ExtractAndConvertIdFromPath(dto.Url), multiplier);
        }
    }
}
