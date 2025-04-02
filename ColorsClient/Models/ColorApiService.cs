using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ColorsClient.Models;

namespace ColorsClient.Models
{
    public class ColorApiService : IColorApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "https://localhost:5001/api/colorpalettes";

        public ColorApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ColorPalette>> GetPalettesAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetStringAsync(_apiUrl);

                var apiResponse = JsonSerializer.Deserialize<ColorPalettes>(
                        response, 
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                return apiResponse?.Palettes ?? new List<ColorPalette>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur API : {ex.Message}");
                return new List<ColorPalette>();
            }
        }
    }
}
