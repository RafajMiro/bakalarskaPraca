// AasService.cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AasApi.Models;

namespace AasApi.Services
{
    public class AasService : IAasService
    {
        private readonly HttpClient _httpClient;

        public AasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AasListResponse> GetAllAasDataAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:8081/shells");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var aasListResponse = JsonSerializer.Deserialize<AasListResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (aasListResponse == null)
            {
                throw new JsonException("AasListResponse returned null");
            }
            return aasListResponse;
        }

        public async Task<AasData> GetAasDataAsync(string shellId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:8081/shells/{shellId}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var aasData = JsonSerializer.Deserialize<AasData>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (aasData == null)
            {
                throw new JsonException("AasData returned null");
            }

            return aasData;
        }

        public async Task<SubmodelData> GetSubmodelDataAsync(string submodelId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:8081/submodels/{submodelId}");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var submodelData = JsonSerializer.Deserialize<SubmodelData>(responseContent, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            if (submodelData == null)
            {
                throw new JsonException("SubmodelData returned null");
            }

            return submodelData;
        }

        public async Task<bool> UpdateSubmodelDataAsync(string submodelId, SubmodelData submodelData)
        {
            Console.WriteLine("Im heeeeerrreeeeeee");
            var content = new StringContent(JsonSerializer.Serialize(submodelData), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:8081/submodels/{submodelId}", content);
            return response.IsSuccessStatusCode;
        }
    }

    public interface IAasService
    {
        Task<AasListResponse> GetAllAasDataAsync();
        Task<AasData> GetAasDataAsync(string shellId);
        Task<SubmodelData> GetSubmodelDataAsync(string submodelId);
        Task<bool> UpdateSubmodelDataAsync(string submodelId, SubmodelData submodelData);
    }
}
