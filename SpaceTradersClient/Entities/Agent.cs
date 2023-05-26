namespace SpaceTradersClient.Entities
{
    public record Agent
    {
        public string AccountId { get; set; }
        public string Symbol { get; set; }
        public string Headquarters { get; set; }
        public long Credits { get; set; }
        public string StartingFaction { get; set; }
    }

    public record MyAgentResponse
    {
        public Agent Data { get; set; }
    }
}
