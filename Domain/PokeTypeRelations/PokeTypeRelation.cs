namespace Domain.PokeTypeRelations
{
    public class PokeTypeRelation
    {
        private PokeTypeRelation(Guid id, int attackingTypeId, int defendingTypeId, double multiplier)
        {
            Id = id;
            AttackingTypeId = attackingTypeId;
            DefendingTypeId = defendingTypeId;
            Multiplier = multiplier;
        }

        protected PokeTypeRelation()
        {
        }

        public Guid Id { get; set; }
        public int AttackingTypeId { get; set; }
        public int DefendingTypeId { get; set; }
        public double Multiplier { get; set; }

        public static PokeTypeRelation Create(Guid id, int attackingTypeId, int defendingTypeId, double multiplier)
        {
            return new PokeTypeRelation(id, attackingTypeId, defendingTypeId, multiplier);
        }

    }
}
