namespace Domain.PokeTypeRelations
{
    public class DamageRelation
    {
        private DamageRelation(Guid id, int attackingTypeId, int defendingTypeId, double multiplier)
        {
            Id = id;
            AttackingTypeId = attackingTypeId;
            DefendingTypeId = defendingTypeId;
            Multiplier = multiplier;
        }

        protected DamageRelation()
        {
        }

        public Guid Id { get; set; }
        public int AttackingTypeId { get; set; }
        public int DefendingTypeId { get; set; }
        public double Multiplier { get; set; }

        public static DamageRelation Create(Guid id, int attackingTypeId, int defendingTypeId, double multiplier)
        {
            return new DamageRelation(id, attackingTypeId, defendingTypeId, multiplier);
        }

    }
}
