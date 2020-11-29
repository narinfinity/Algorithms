namespace game
{
    public struct Location
    {
        public Location(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }

        public bool Equals(Location location)
        {
            return Row == location.Row
                && Column == location.Column;
        }
    }
}