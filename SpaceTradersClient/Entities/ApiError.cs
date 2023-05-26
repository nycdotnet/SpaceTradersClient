namespace SpaceTradersClient.Entities
{
    public record ApiError
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
