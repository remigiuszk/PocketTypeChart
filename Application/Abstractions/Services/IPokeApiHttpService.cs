using Application.External.PokeApi.Dto;

namespace Application.Abstractions.Services
{
    public interface IPokeApiHttpService
    {
        Task<ExternalPokeTypeDto?> GetPokeTypeAsync(int id);
        Task<byte[]?> DownloadSpriteAsync(string url);
    }
}