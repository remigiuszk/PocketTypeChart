using Application.PokeTypes.PreloadTypes.Services.Dto;

namespace Application.Abstractions.Services
{
    public interface IPokeApiHttpService
    {
        Task<PokeTypeDto> GetPokeType(int id);
    }
}