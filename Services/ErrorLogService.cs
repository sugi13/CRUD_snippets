

using System.Net.Http.Json;

namespace CRUDWithFluxor.Services;

public class ErrorLogService
{
    private readonly HttpClient _httpClient;

    public ErrorLogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task LogErrorAsync(Exception exception)
    {
        var request = new
        {
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            Source = "Blazor WASM"
        };

        using var response = await _httpClient.PostAsJsonAsync(
            "https://localhost:7015/api/errors",
            request);

        response.EnsureSuccessStatusCode();
    }
}
