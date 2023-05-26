namespace SpaceTradersClient.Entities
{
    public record Agent
    {
        public string AccountId { get; set; }
        public string Symbol { get; set; }
        public string Headquarters { get; set; }
        public Entity? HeadquartersSystem()
        {
            if (Waypoint.TryParseWaypoint(Headquarters, out _, out var system, out _))
            {
                return new Entity { Symbol = system };
            }
            return null;
        }
        public Entity? HeadquartersWaypoint()
        {
            if (Waypoint.TryParseWaypoint(Headquarters, out _, out var _, out var waypoint))
            {
                return new Entity { Symbol = waypoint };
            }
            return null;
        }

        public long Credits { get; set; }
        public string StartingFaction { get; set; }
    }

    public record MyAgentResponse
    {
        public Agent Data { get; set; }
    }
}
