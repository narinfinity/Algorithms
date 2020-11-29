using System.Collections.Generic;
using System.Linq;

namespace game
{
    public class SquireUnit
    {
        public SquireUnit(Location location)
        {
            Location = location;
            Walls = new List<Wall>();
        }
        public Location Location { get; }
        public Hole Hole { get; private set; }
        public Marble Marble { get; set; }
        public List<Wall> Walls { get; private set; }
        public bool HasHole => Hole != null;
        public bool HasEmptyHole => HasHole && Hole.IsEmpty;
        public bool HasMarble => Marble != null;
        public bool CanLocateMarbleAt(Location marbleLocation)
        {
            return !HasEmptyHole
                && Location.Row == marbleLocation.Row
                && Location.Column == marbleLocation.Column;
        }

        public bool TryLocateHole(Hole hole = null)
        {
            var inSameLocation = hole != null
                && Location.Row == hole.Location.Row
                && Location.Column == hole.Location.Column;
            if (inSameLocation) Hole = hole;
            return HasHole;
        }

        public bool HasWall(MoveDirection marbleMoveDirection)
        {
            return Walls.Any(wall =>
            {
                switch (marbleMoveDirection)
                {
                    case MoveDirection.N:
                        return wall.IsBoardEdge(marbleMoveDirection)
                        ||
                            (Location.Row == wall.Location2.Row
                            && Location.Column == wall.Location2.Column
                            && wall.Location1.Column == wall.Location2.Column);
                    case MoveDirection.S:
                        return wall.IsBoardEdge(marbleMoveDirection)
                        ||
                            (Location.Row == wall.Location1.Row
                            && Location.Column == wall.Location1.Column
                            && wall.Location1.Column == wall.Location2.Column);
                    case MoveDirection.E:
                        return wall.IsBoardEdge(marbleMoveDirection)
                        ||
                            (Location.Column == wall.Location1.Column
                            && Location.Row == wall.Location1.Row
                            && wall.Location1.Row == wall.Location2.Row);
                    case MoveDirection.W:
                        return wall.IsBoardEdge(marbleMoveDirection)
                        ||
                            (Location.Column == wall.Location2.Column
                            && Location.Row == wall.Location1.Row
                            && wall.Location1.Row == wall.Location2.Row);
                    default:
                        return false;
                }
            });
        }
    }
}