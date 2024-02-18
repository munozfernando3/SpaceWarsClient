using Newtonsoft.Json;
using System.Net.Http.Json;

namespace SpaceWarsServices;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private string token;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<JoinGameResponse> JoinGameAsync(string name)
    {
        int retryCount = 0;
        while (retryCount < 3) // Retry up to 3 times for transient errors
        {
            try
            {
                var response = await _httpClient.GetAsync($"/game/join?name={Uri.EscapeDataString(name)}");

                response.EnsureSuccessStatusCode(); // Throw an exception if the status code is not a success code

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JoinGameResponse>(content);
                token = result.Token; // Consider securely storing the token

                Console.WriteLine("Successfully joined the game."); // User feedback
                return result;
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                // Handle specific HTTP errors (e.g., 503 Service Unavailable)
                Console.WriteLine("Service is unavailable. Retrying...");
                retryCount++;
                await Task.Delay(2000); // Wait 2 seconds before retrying
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                if (retryCount >= 2) throw; // Rethrow the exception on the last retry attempt
                retryCount++;
            }
        }

        return null; // Return null if unable to join after retries
    }

    public async Task QueueAction(IEnumerable<QueueActionRequest> action)
    {
        try
        {
            string url = $"/game/{token}/queue";
            var response = await _httpClient.PostAsJsonAsync(url, action);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public async Task ClearAction()
    {
        try
        {
            string url = $"/game/{token}/queue/clear";
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public async Task<IEnumerable<GameMessage>> ReadAndEmptyMessages()
    {
        try
        {
            string url = $"/game/playermessages?token={token}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<GameMessage>>(url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}
