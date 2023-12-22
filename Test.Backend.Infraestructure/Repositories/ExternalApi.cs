namespace Test.Backend.Infraestructure.Repositories
{
    using System.Net.Http.Json;
    using Test.Backend.Application.Contracts.Persistence;
    using Test.Backend.Domain.ExternalApi;
    public class ExternalApi : IExternalApi
    {
        private readonly HttpClient _httpClient;

        public ExternalApi()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<DiscountProduct>> GetDataFromMockapi()
        {
            var response = await _httpClient.GetAsync("https://658310b502f747c8367afe14.mockapi.io/api/discountProductById/discount"); 
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DiscountProduct>>();
                return result;
            }
            return null;
        }
    }
}
