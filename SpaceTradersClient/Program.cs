using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace SpaceTradersClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = GetConfig();
            var httpClient = GetHttpClient(config);

            var stc = new SpaceTradersClient(config, httpClient);

            var agent = await stc.MyAgent();

            agent.Switch(agent => {
                Console.WriteLine($"My agent has {agent.Credits} credits and is named {agent.Symbol}.");
            },
            _ => {
                Console.WriteLine("Could not get an agent.");
            });
        }


        private static Config GetConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appconfig.local.json")
                .Build();
            return config.GetSection("Settings").Get<Config>() ?? throw new ApplicationException("Invalid configuration");
        }

        private static HttpClient GetHttpClient(Config config)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.spacetraders.io/v2/"),
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", config.ApiKey);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Steve's Space Traders Client");
            return httpClient;
        }
    }
}