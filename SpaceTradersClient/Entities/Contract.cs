namespace SpaceTradersClient.Entities
{
    public record Contract
    {
        public string Id { get; set; }
        public string FactionSymbol { get; set; }
        public string Type { get; set; }
        public ContractTerms Terms { get; set; }
        public bool Accepted { get; set; }
        public bool Fulfilled { get; set; }
        public DateTimeOffset Expiration { get; set; }
        public DateTimeOffset DeadlineToAccept { get; set; }
    }

    public record ContractTerms
    {
        public DateTimeOffset Deadline { get; set; }
        public ContractPayment Payment { get; set; }
        public ContractDeliver[] Deliver { get; set; }
    }

    public record ContractPayment
    {
        public long OnAccepted { get; set; }
        public long OnFulfilled { get; set; }
    }

    public record ContractDeliver
    {
        public string TradeSymbol { get; set; }
        public string DestinationSymbol { get; set; }
        public long UnitsRequired { get; set; }
        public long UnitsFulfilled { get; set; }
    }

    public record MyContractsResponse
    {
        public Contract[] Data { get; set; }
        public ListMeta Meta { get; set; }
    }
}
