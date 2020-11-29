namespace game
{
    public class Wall
    {
        public (Location, Location) NorthEdge =>
            (new Location(-1, Location2.Column), new Location(0, Location2.Column));
        public (Location, Location) SouthEdge =>
            (new Location(Location1.Row, Location1.Column), new Location(-10, Location1.Column));
        public (Location, Location) EastEdge =>
            (new Location(Location1.Row, Location1.Column), new Location(Location1.Row, -10));
        public (Location, Location) WestEdge =>
            (new Location(Location2.Row, -1), new Location(Location2.Row, 0));

        public Wall(Location location1, Location location2)
        {
            Location1 = location1;
            Location2 = location2;
        }

        public Location Location1 { get; }
        public Location Location2 { get; }

        public Wall Copy()
        {
            return new Wall(Location1, Location2);
        }
        public bool IsBoardEdge(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.N:
                    return Location1.Equals(NorthEdge.Item1) && Location2.Equals(NorthEdge.Item2);
                case MoveDirection.S:
                    return Location2.Equals(SouthEdge.Item1) && Location2.Equals(SouthEdge.Item2);
                case MoveDirection.E:
                    return Location2.Equals(EastEdge.Item1) && Location2.Equals(EastEdge.Item2);
                case MoveDirection.W:
                    return Location1.Equals(WestEdge.Item1) && Location2.Equals(WestEdge.Item2);
                default:
                    return false;
            }
        }
    }
}