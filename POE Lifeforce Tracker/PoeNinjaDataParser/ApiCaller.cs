namespace POE_Lifeforce_Tracker.PoeNinjaDataParser;

public class ApiCaller
{
    private static HttpClient _client = new HttpClient();

    public static async Task<string> ItemGetter(string url)
    {
        return await _client.GetStringAsync(url);
    }
}