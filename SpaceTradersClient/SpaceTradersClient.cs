using OneOf;
using OneOf.Types;
using SpaceTradersClient.Entities;
using System.Text.Json;

namespace SpaceTradersClient
{
    public class SpaceTradersClient
    {
        private readonly Config config;
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jsonOptions;

        public SpaceTradersClient(Config config, HttpClient httpClient)
        {
            this.config = config;
            this.httpClient = httpClient;
            jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<OneOf<Agent, None>> MyAgent()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "my/agent");
            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using var stream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<MyAgentResponse>(stream, jsonOptions);
            if (responseObject?.Data is null)
            {
                return new None();
            }
            return responseObject.Data;
        }

        public async Task<OneOf<Waypoint, None>> GetWaypoint(Entity System, Entity Waypoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"systems/{System.Symbol}/waypoints/{Waypoint.Symbol}");
            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using var stream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetWaypointResponse>(stream, jsonOptions);
            if (responseObject?.Data is null)
            {
                return new None();
            }
            return responseObject.Data;
        }

        public async Task<OneOf<Waypoint[], None>> WaypointsInSystem(Entity System)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"systems/{System.Symbol}/waypoints");
            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            //var dbg = await response.Content.ReadAsStringAsync();
            using var stream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<WaypointsInSystemResponse>(stream, jsonOptions);
            if (responseObject?.Data is null)
            {
                return new None();
            }
            return responseObject.Data;
        }

        public async Task<OneOf<Contract[], None>> MyContracts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "my/contracts");
            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using var stream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<MyContractsResponse>(stream, jsonOptions);
            if (responseObject?.Data is null)
            {
                return new None();
            }
            // TODO: implement pagination
            return responseObject.Data;
        }

        public async Task<OneOf<AcceptContractData, AlreadyAccepted>> AcceptContract(string ContractId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"my/contracts/{ContractId}/accept");
            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            using var stream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<AcceptContractResponse>(stream, jsonOptions);
            if (responseObject?.Error is not null)
            {
                if (responseObject.Error.Message.EndsWith("has already been accepted."))
                {
                    return new AlreadyAccepted();
                }
                throw new ApplicationException(responseObject.Error.Message);
            }
            if (responseObject?.Data is not null)
            {
                return responseObject.Data;
            }
            throw new ApplicationException("not sure how we got here...");
        }
    }
}
