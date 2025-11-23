using Application.Shared;

namespace Application.PokeTypes.PreloadTypes
{
    internal static class PreloadTypesErrors
    {
        public static readonly Error TypeAlreadyExists = new(
            "PreloadTypes.AlreadyExists",
            "This type already exists in the database");
    }
}
