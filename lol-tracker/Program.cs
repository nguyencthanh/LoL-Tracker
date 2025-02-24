using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;


namespace LoLTracker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Getting API Key
            string apiKey = ApiKey.GetApiKey();

            HttpClient client = new HttpClient();

            // Obtain User Information 
            Console.WriteLine("What is your game name: ");
            string gameName = Console.ReadLine();
            Console.WriteLine("What is your tag name: ");
            string tagLine = Console.ReadLine();
            string url = "https://americas.api.riotgames.com/riot/account/v1/accounts/by-riot-id/" + gameName + "/" + tagLine + "?api_key=" + apiKey;

            try
            {
                // Performing the GET request
                HttpResponseMessage respone = await client.GetAsync(url);

                // If the request is successful
                if (respone.IsSuccessStatusCode)
                {
                    string content = await respone.Content.ReadAsStringAsync();

                    Account account = JsonSerializer.Deserialize<Account>(content);

                    Console.WriteLine($"Puuid: {account.puuid}");
                    Console.WriteLine($"Puuid: {account.gameName}");
                    Console.WriteLine($"Puuid: {account.tagLine}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}