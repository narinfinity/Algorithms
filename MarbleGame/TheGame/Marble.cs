using System;
using System.Collections.Generic;

namespace game
{
    public class Marble
    {
        public static IDictionary<MoveDirection, MoveDirection> OppositeDirections = createMarbleMoveDirections();
        public Marble(int number, Location location)
        {
            Number = number;
            Location = location;
        }
        public int Number { get; }
        public Location Location { get; set; }

        public Location NextLocation(MoveDirection marbleMoveDirection)
        {
            var (row, col) = (Location.Row, Location.Column);
            switch (marbleMoveDirection)
            {
                case MoveDirection.N: return new Location(row - 1, col);
                case MoveDirection.S: return new Location(row + 1, col);
                case MoveDirection.E: return new Location(row, col + 1);
                case MoveDirection.W: return new Location(row, col - 1);
                default: return new Location(-1, -1);
            }
        }
        public Marble Copy()
        {
            return new Marble(Number, Location);
        }
        private static IDictionary<MoveDirection, MoveDirection> createMarbleMoveDirections()
        {
            return new Dictionary<MoveDirection, MoveDirection>
            {
                { MoveDirection.N, MoveDirection.S },
                { MoveDirection.S, MoveDirection.N },
                { MoveDirection.W, MoveDirection.E },
                { MoveDirection.E, MoveDirection.W }
            };
        }
    }

    public class MarbleOrderingComparer : IComparer<Marble>
    {
        private MoveDirection Direction;
        public MarbleOrderingComparer(MoveDirection direction)
        {
            Direction = direction;
        }
        public int Compare(Marble a, Marble b)
        {
            switch (Direction)
            {
                case MoveDirection.N:
                    return a.Location.Row == b.Location.Row
                        ? 0
                        : a.Location.Row < b.Location.Row ? -1 : 1;
                case MoveDirection.S:
                    return a.Location.Row == b.Location.Row
                        ? 0
                        : a.Location.Row < b.Location.Row ? 1 : -1;
                case MoveDirection.E:
                    return a.Location.Column == b.Location.Column
                        ? 0
                        : a.Location.Column < b.Location.Column ? 1 : -1;
                case MoveDirection.W:
                    return a.Location.Column == b.Location.Column
                        ? 0
                        : a.Location.Column < b.Location.Column ? -1 : 1;
                default:
                    return 0;
            }
        }
    }
}