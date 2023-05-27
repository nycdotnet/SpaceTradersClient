namespace SpaceTradersClient.Entities
{
    public record Trait : SymbolEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
