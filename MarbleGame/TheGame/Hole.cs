namespace game
{
    public class Hole
    {
        public Hole(int number, Location location)
        {
            Number = number;
            Location = location;
        }
        public int Number { get; }
        public Location Location { get; }
        public Marble Marble { get; set; }
        public bool IsEmpty => Marble == null;
        public bool CanLocateMarbleAt(Location marbleLocation)
        {
            return IsEmpty
                && Location.Row == marbleLocation.Row
                && Location.Column == marbleLocation.Column;
        }
        public bool HasSameNumberAs(int marbleNumber)
        {
            return Number == marbleNumber;
        }
        public Hole Copy()
        {
            return new Hole(Number, Location)
            {
                Marble = IsEmpty ? null : Marble.Copy()
            };
        }
    }
}