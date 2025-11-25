using Application.Shared;

namespace Application.PokeTypes.PreloadTypes
{
    internal static class PreloadTypesErrors
    {
        public static readonly Error TypesInDb = new(
            "PreloadTypes.TypesAlreadyExist",
            "There are types already present in the database. To ensure data completion clear the existing records first.");

        public static readonly Error DamageRelationsInDb = new(
           "PreloadTypes.DamageRelationsAlreadyExist",
           "There are damage relations already present in the database. To ensure data completion clear the existing records first.");
    }
}
