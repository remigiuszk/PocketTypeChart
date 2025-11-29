using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation.PokeType;

namespace Application.DamageRelations.GetDamageRelationsForSelectedTypes.ReadModel
{
    public class TypingEffectivenessReadModel
    {
        public List<PokeTypeReadModel> SelectedTypes { get; set; }
        public List<DefensiveDamageRelationReadModel> DefensiveDamageRelations { get; set; }
        public List<OffensiveDamageRelationReadModel> OffensiveDamageRelations { get; set; }
    }
}
