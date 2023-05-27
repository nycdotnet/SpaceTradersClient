using System.Text.Json.Nodes;

namespace SpaceTradersClient.Entities
{
    public record Ship : SymbolEntity
    {
        public NavigationInfo Nav { get; set; }
        public JsonObject Crew { get; set; }
        public JsonObject Fuel { get; set; }
        public JsonObject Frame { get; set; }
        public JsonObject Reactor { get; set; }
        public JsonObject Engine { get; set; }
        public JsonArray Modules { get; set; }
        public JsonArray Mounts { get; set; }
        public JsonObject Registration { get; set; }
        public JsonObject Cargo { get; set; }
    }
    

    public record NavigationInfo
    {
        public string SystemSymbol { get; set; }
        public string WaypointSymbol { get; set; }
        public string Status { get; set; }
        public string FlightMode { get; set; }
    }

    public record Route
    {
        public Location Departure { get; set; }
        public Location Destination { get; set; }
        public DateTimeOffset Arrival { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
    }

    public record PurchaseShipRequest
    {
        public string ShipType { get; set; }
        public string WaypointSymbol { get; set; }
    }

    public record PurchaseShipResponse
    {
        public PurchaseShipResponseData Data { get; set; }

        public record PurchaseShipResponseData
        {
            public Agent Agent { get; set; }
            public Ship Ship { get; set; }
            public Transaction Transaction { get; set; }
        }
    }
}
