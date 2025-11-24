namespace Domain.PokeTypeRelations
{
    public class DamageRelation
    {
        private DamageRelation(int attackingTypeId, int defendingTypeId, double multiplier)
        {
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

        public static DamageRelation Create(int attackingTypeId, int defendingTypeId, double multiplier)
        {
            return new DamageRelation(attackingTypeId, defendingTypeId, multiplier);
        }

    }
}
