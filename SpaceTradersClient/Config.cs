using System.Text.Json;

namespace SpaceTradersClient
{
    public record Config
    {
        public required string ApiKey { get; set; }
    }
}
