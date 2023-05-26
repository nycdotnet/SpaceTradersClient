namespace SpaceTradersClient.Entities
{
    public record Chart
    {
        public string SubmittedBy { get; set; }
        public DateTimeOffset SubmittedOn { get; set; }
    }
}
