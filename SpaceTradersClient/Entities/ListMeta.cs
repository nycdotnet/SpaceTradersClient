namespace SpaceTradersClient.Entities
{
    public record ListMeta
    {
        public long Total { get; set; }
        public long Page { get; set; }
        public long Limit { get; set; }
    }
}
