using Newtonsoft.Json;

namespace POE_Lifeforce_Tracker.PoeNinjaDataParser;

public class PoeNinjaParser
{
    // this will create url based on the type of item provided
    private static string TypeToUrl(string poeNinjaDirectory, string type)
    {
        if (type == "currency")
        {
            string url =
                "https://poe.ninja/api/data/currencyoverview?league=Sanctum&type=" + poeNinjaDirectory + "&language=en";
            return url;
        }
        else
        {
            string url =
                "https://poe.ninja/api/data/itemoverview?league=Sanctum&type=" + poeNinjaDirectory + "&language=en";
            return url;
        }
    }
    
    // this will deserialize data from poe.ninja based on PoeDataTypes and url provided by TypeToUrl 
    public static async Task GetDataFromPoeNinja (string[] ItemType, string type)
    {
        foreach (var directory in ItemType)
        {
            string url = TypeToUrl(directory, type);
            Console.WriteLine(url);

            string poeNinjaJson = await ApiCaller.ItemGetter(url);

            dynamic deserializedPoeNinjaJson = JsonConvert.DeserializeObject(poeNinjaJson);

            foreach (var line in deserializedPoeNinjaJson.lines)
            {
                string itemTypeName = (type == "currency") ? line.currencyTypeName : line.name;
                string priceInChaos = (type == "currency") ? line.chaosEquivalent : line.chaosValue;
                Console.WriteLine($"{itemTypeName} {priceInChaos}");
            }
        }
    }
}