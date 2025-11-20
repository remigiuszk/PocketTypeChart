namespace Domain.PokeTypes
{
    public class PokeType
    {
        private PokeType(int id, string? name, string? image)
        {
            Id = id;
            Name = name;
            Image = image;
        }

        protected PokeType()
        {
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }

        public static PokeType Create(int id, string? name, string? image)
        {
            return new PokeType(id, name, image);
        }
    }
}
