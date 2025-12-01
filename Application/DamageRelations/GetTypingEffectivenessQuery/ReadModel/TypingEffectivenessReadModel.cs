using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation;

namespace Application.DamageRelations.GetDamageRelationsForSelectedTypes.ReadModel
{
    public class TypingEffectivenessReadModel
    {
        public List<DefensiveDamageRelationReadModel> DefensiveDamageRelations { get; set; } = default!;
        public List<OffensiveDamageRelationReadModel> OffensiveDamageRelations { get; set; } = default!;
    }
}
