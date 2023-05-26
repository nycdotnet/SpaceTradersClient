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
    }
}
