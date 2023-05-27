namespace SpaceTradersClient.Entities
{
    public record Transaction
    {
        public string ShipSymbol { get; set; }
        public string WaypointSymbol { get; set; }
        public string AgentSymbol { get; set; }
        public long Price { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
