using Application.Abstractions.Services;
using Application.External.Constants;
using Application.External.Dto;
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

        public async Task<PokeTypeDto> GetPokeTypeAsync(int id)
        {
            var url = PokeApiConstants.TYPES_ENDPOINT + id.ToString();

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var t = await response.Content.ReadFromJsonAsync<PokeTypeDto>();
                return t;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
