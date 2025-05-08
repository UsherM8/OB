using System.Net.Http.Json;
using Domain.Dtos;

namespace Dal.Services;

public class RdwApiClient
{
    private readonly HttpClient _httpClient;

    public RdwApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RdwCarDto> GetCarAsync(string licensePlate)
    {
        var response = await _httpClient.GetAsync($"m9d7-ebf2.json?kenteken={licensePlate}");
        response.EnsureSuccessStatusCode();
        var carDtos = await response.Content.ReadFromJsonAsync<List<RdwCarDto>>();
        if (carDtos == null || carDtos.Count == 0)
        {
            throw new Exception("No car data found for the given license plate.");
        }
        return carDtos.First();
    }
}