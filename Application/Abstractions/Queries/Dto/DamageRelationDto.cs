namespace Application.Abstractions.Queries.Dto
{
    public class DamageRelationDto
    {
        public DamageRelationDto(PokeTypeDto attackingType, PokeTypeDto defendingType, double multiplier)
        {
            AttackingType = attackingType;
            DefendingType = defendingType;
            Multiplier = multiplier;
        }

        public PokeTypeDto AttackingType { get; set; }
        public PokeTypeDto DefendingType { get; set; }
        public double Multiplier { get; set; }
    }
}
