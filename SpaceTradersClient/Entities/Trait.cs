namespace SpaceTradersClient.Entities
{
    public record Trait : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
