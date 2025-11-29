using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation.PokeType;
using Domain.PokeTypeRelations;
using Domain.PokeTypes;

namespace Application.DamageRelations.GetTypingEffectivenessQuery.Mappers
{
    public static class TypingEffectivenessMapper
    {
        public static DefensiveDamageRelationReadModel ToDefensiveRelationReadModel(this IGrouping<int, DamageRelation> grouping, PokeType pokeType)
        {
            return new DefensiveDamageRelationReadModel()
            {
                Multiplier = CalculateMultiplier(grouping),
                AtackingType = pokeType.ToReadModel()
            };
        }

        private static double CalculateMultiplier(IGrouping<int, DamageRelation> grouping)
        {
            return grouping.
                Select(x => x.Multiplier).
                Aggregate(1.0, (totalMultiPlayer, currentMultiplier) => totalMultiPlayer * currentMultiplier);
        }

        private static PokeTypeReadModel ToReadModel(this PokeType pokeType)
        {
            return new PokeTypeReadModel(pokeType.Id, pokeType.Name, pokeType.Sprite);
        }
    }
}
