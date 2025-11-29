namespace Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation.PokeType
{
    public class PokeTypeReadModel
    {
        public PokeTypeReadModel(int id, string name, string sprite)
        {
            Id = id;
            Name = name;
            Sprite = sprite;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sprite { get; set; }

    }
}
