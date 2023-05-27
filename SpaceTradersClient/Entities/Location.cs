namespace SpaceTradersClient.Entities
{
    public record Location : SymbolEntity
    {
        public string Type { get; set; }
        public string SystemSymbol { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
    }
}
