namespace SpaceTradersClient.Entities
{
    public record ShipyardResponse
    {
        public ShipyardInfo Data { get; set; }
    }

    public record ShipyardInfo : SymbolEntity
    {
        public ShipType[] ShipTypes { get; set; }
    }

    public record ShipType
    {
        public string Type { get; set; }
    }
}
