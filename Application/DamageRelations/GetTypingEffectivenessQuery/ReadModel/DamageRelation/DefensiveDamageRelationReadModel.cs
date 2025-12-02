using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation.PokeType;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.NewFolder;

namespace Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation
{
    public class DefensiveDamageRelationReadModel : DamageRelationReadModel
    {
        public PokeTypeReadModel AttackingType { get; set; } = default!;
    }
}
