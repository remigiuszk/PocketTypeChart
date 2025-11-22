using Application.PokeTypes.PreloadTypes.Services.Constants;

namespace Application.PokeTypes.PreloadTypes.Services
{
    public class PokeApiHttpService
    {
        private readonly HttpClient _httpClient;

        public PokeApiHttpService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(PokeApiConstants.BASE_URL)
            };
        }
    }
}
