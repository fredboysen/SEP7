using System.Net.Http.Json;
using SEP7.WebAPI.Models;


namespace SEP7.WebAPI.Services;
public class HQ_UsageService
{
    private readonly HttpClient _httpClient;

    public HQ_UsageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<HQ_Usage>> GetHQUsagesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<HQ_Usage>>("api/HQ_Usage");
        return response ?? new List<HQ_Usage>();
    }
}
