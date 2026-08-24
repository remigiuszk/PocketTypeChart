using Application.Abstractions.Queries.Dto;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation.PokeType;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.NewFolder;

namespace Application.DamageRelations.GetTypingEffectivenessQuery.Mappers
{
    public static class TypingEffectivenessMapper
    {
        public static List<DefensiveDamageRelationReadModel> MapDefensiveRelations(this List<DamageRelationDto> relations, List<int> selectedTypes)
        {
            var returnList = new List<DefensiveDamageRelationReadModel>();

            var defensiveRelationsWithSelectedTypes = relations.Where(x => selectedTypes.Contains(x.DefendingType.Id));
            var relationsGroupedByAttackingType = defensiveRelationsWithSelectedTypes.GroupBy(x => x.AttackingType.Id);

            foreach (var grouping in relationsGroupedByAttackingType)
            {
                var relation = new DefensiveDamageRelationReadModel()
                {
                    AttackingType = PokeTypeReadModel.CreateFromDto(grouping.FirstOrDefault()!.AttackingType),
                    Multiplier = CalculateMultiplier(grouping)
                };

                if (RelationIsNotNeutral(relation))
                    returnList.Add(relation);
            }

            return returnList;
        }

        public static List<OffensiveDamageRelationReadModel> MapOffensiveRelations(this List<DamageRelationDto> relations, List<int> selectedTypes)
        {
            var returnList = new List<OffensiveDamageRelationReadModel>();

            var offensiveRelationsWithSelectedTypes = relations.Where(x => selectedTypes.Contains(x.AttackingType.Id));
            var relationsGroupedByAttackingAndDefendingType = offensiveRelationsWithSelectedTypes
                .GroupBy(x => (x.AttackingType.Id, x.DefendingType.Id));

            foreach (var grouping in relationsGroupedByAttackingAndDefendingType)
            {
                var relation = new OffensiveDamageRelationReadModel()
                {
                    DefendingType = PokeTypeReadModel.CreateFromDto(grouping.FirstOrDefault()!.DefendingType),
                    AttackingMoveType = PokeTypeReadModel.CreateFromDto(grouping.FirstOrDefault()!.AttackingType),
                    Multiplier = grouping.FirstOrDefault()!.Multiplier
                };

                if (RelationIsNotNeutral(relation))
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

        private static bool RelationIsNotNeutral(DamageRelationReadModel relation)
        {
            return relation.Multiplier != 1;
        }
    }
}
