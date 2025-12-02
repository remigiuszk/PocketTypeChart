using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation.PokeType;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.NewFolder;

namespace Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation
{
    public class OffensiveDamageRelationReadModel : DamageRelationReadModel
    {
        public PokeTypeReadModel AttackingMoveType { get; set; }
        public PokeTypeReadModel DefendingType { get; set; }
    }
}
