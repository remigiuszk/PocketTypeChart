using Application.External.Dto;

namespace Application.Abstractions.Services
{
    public interface IPokeApiHttpService
    {
        Task<PokeTypeDto> GetPokeTypeAsync(int id);
    }
}