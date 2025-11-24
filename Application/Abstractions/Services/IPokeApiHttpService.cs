using Application.External.PokeApi.Dto;

namespace Application.Abstractions.Services
{
    public interface IPokeApiHttpService
    {
        Task<PokeTypeDto> GetPokeTypeAsync(int id);
    }
}