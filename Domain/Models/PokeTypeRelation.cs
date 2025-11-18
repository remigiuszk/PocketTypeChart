namespace Domain.Models
{
    public class PokeTypeRelation
    {
        public Guid Id { get; set; }
        public int AttackingTypeId { get; set; }
        public int DefendingTypeId { get; set; }
        public double Multiplier { get; set; }
    }
}
