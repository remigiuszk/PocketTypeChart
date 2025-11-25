using Application.Abstractions.Services;
using Application.External.PokeApi.Constants;
using Application.External.PokeApi.Dto;
using System.Net.Http.Json;

namespace Application.External.PokeApi
{
    public class PokeApiHttpService : IPokeApiHttpService
    {
        private readonly HttpClient _httpClient;

        public PokeApiHttpService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(PokeApiConstants.BASE_URL)
            };
        }

        public async Task<PokeTypeDto?> GetPokeTypeAsync(int id)
        {
            var url = PokeApiConstants.TYPES_ENDPOINT + id.ToString();

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var dto = await response.Content.ReadFromJsonAsync<PokeTypeDto>();
                return dto;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
