using Application.Abstractions.Queries.Dto;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation.PokeType;

namespace Application.DamageRelations.GetTypingEffectivenessQuery.Mappers
{
    public static class TypingEffectivenessMapper
    {
        public static List<DefensiveDamageRelationReadModel> MapDefensiveRelations(this List<DamageRelationDto> relations, List<int> selectedTypes)
        {
            var returnList = new List<DefensiveDamageRelationReadModel>();

            var lookup = relations.Where(x => selectedTypes.Contains(x.DefendingType.Id)).ToLookup(x => x.AttackingType.Id);

            foreach (var grouping in lookup)
            {
                var relation = new DefensiveDamageRelationReadModel()
                {
                    AtackingType = PokeTypeReadModel.CreateFromDto(grouping.FirstOrDefault()!.AttackingType),
                    Multiplier = CalculateMultiplier(grouping)
                };

                if (relation.Multiplier != 1)
                    returnList.Add(relation);
            }

            return returnList;
        }

        public static List<OffensiveDamageRelationReadModel> MapOffensiveRelations(this List<DamageRelationDto> relations, List<int> selectedTypes)
        {
            var returnList = new List<OffensiveDamageRelationReadModel>();

            var lookup = relations.Where(x => selectedTypes.Contains(x.DefendingType.Id)).ToLookup(x => x.DefendingType.Id);

            foreach (var grouping in lookup)
            {
                var relation = new OffensiveDamageRelationReadModel()
                {
                    DefendingType = PokeTypeReadModel.CreateFromDto(grouping.FirstOrDefault()!.DefendingType),
                    Multiplier = CalculateMultiplier(grouping)
                };

                if (relation.Multiplier != 1)
                    returnList.Add(relation);
            }

            return returnList;
        }

        private static double CalculateMultiplier(IGrouping<int, DamageRelationDto> grouping)
        {
            return grouping.
                Select(x => x.Multiplier).
                Aggregate(1.0, (totalMultiPlayer, currentMultiplier) => totalMultiPlayer * currentMultiplier);
        }
    }
}
