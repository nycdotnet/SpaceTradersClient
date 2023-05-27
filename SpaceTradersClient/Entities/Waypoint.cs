namespace SpaceTradersClient.Entities
{
    public record Waypoint : SymbolEntity
    {
        public string SystemSymbol { get; set; }
        public string Type { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public SymbolEntity[] Orbitals { get; set; }
        public Trait[] Traits { get; set; }
        public Chart Chart { get; set; }
        public SymbolEntity Faction { get; set; }

        // todo: learn how to make a null hint where the out vars will be null only if it returns false
        public static bool TryParseWaypoint(string Value, out string? Sector, out string? System, out string? Waypoint)
        {
            Sector = null;
            System = null;
            Waypoint = null;
            if (string.IsNullOrEmpty(Value))
            {
                return false;
            }
            var elements = Value.Split('-', 3);
            if (elements.Length != 3)
            {
                return false;
            }
            Sector = elements[0];
            System = $"{elements[0]}-{elements[1]}";
            Waypoint = Value;
            return true;
        }
    }

    public record GetWaypointResponse
    {
        public Waypoint Data { get; set; }
    }

    public record WaypointsInSystemResponse
    {
        public Waypoint[] Data { get; set; }
    }
}
