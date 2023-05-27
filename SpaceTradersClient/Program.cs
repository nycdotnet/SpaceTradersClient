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

            if (!(await stc.MyAgent()).TryPickT0(out var agent, out _))
            {
                Console.WriteLine("Could not get an agent.");
                return;
            }

            Console.WriteLine($"My agent has {agent.Credits} credits and is named {agent.Symbol}.");
            Console.WriteLine($"My agent's HQ is at waypoint {agent.Headquarters}.");

            if (!(await stc.GetWaypoint(agent.HeadquartersSystem(), agent.HeadquartersWaypoint())).TryPickT0(out var waypoint, out _)) {
                Console.WriteLine("Could not get waypoint data.");
                return;
            }

            Console.WriteLine($"My agent's HQ is on a {waypoint.Type}");

            if (!(await stc.MyContracts()).TryPickT0(out var contracts, out _))
            {
                Console.WriteLine("Could not get contract data.");
                return;
            }

            Console.WriteLine($"My agent has {contracts.Length} contract(s)");

            if ((await stc.AcceptContract(contracts[0].Id)).TryPickT0(out var acceptContract, out var alreadyAccepted))
            {
                Console.WriteLine($"Accepted contract {acceptContract.Contract.Id}");
            }
            else
            {
                Console.WriteLine("This contract was already accepted.");
            }

            var wayPointsInSystem = await stc.WaypointsInSystem(agent.HeadquartersSystem());

            var shipyardInSystem = wayPointsInSystem.AsT0.Where(wp => wp.Traits.Any(t => t.Symbol == "SHIPYARD")).FirstOrDefault();
            if (shipyardInSystem is null)
            {
                Console.WriteLine("no shipyard found");
                return;
            }
            else
            {
                Console.WriteLine($"There is a shipyard in this system at waypoint ${shipyardInSystem.Symbol} which is a {shipyardInSystem.Type}");
            }

            var shipsInShipyard = await stc.GetShipTypesInShipyard(shipyardInSystem.SystemSymbol, shipyardInSystem.Symbol);

            if (shipsInShipyard.TryPickT0(out var shipyardInfo, out var _))
            {
                Console.WriteLine($"Found the following kinds of ships: { string.Join(", ", shipyardInfo.ShipTypes.Select(st => st.Type)) }.");
                if (shipyardInfo.ShipTypes.Any(st => st.Type == "SHIP_MINING_DRONE"))
                {
                    Console.WriteLine("OK there is a mining drone!");
                }
                else
                {
                    Console.WriteLine("No mining drone!");
                    return;
                }
            }

            // TODO: add ship purchasing code.

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