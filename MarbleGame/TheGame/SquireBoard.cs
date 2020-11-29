using System;
using System.Collections.Generic;
using System.Linq;

namespace game
{
    public class SquireBoard
    {
        public SquireBoard(TestCaseInputDataModel input)
        {
            Size = input.SquireUnits.GetLength(0);
            SquireUnits = input.SquireUnits;
            Marbles = input.Marbles;
            Holes = input.Holes;
            SetWallsForSquires(input.Walls);
            SetHolesForSquires();
            Moves = new List<Move>();
        }
        public int Size { get; }
        public SquireUnit[,] SquireUnits { get; }
        public List<Hole> Holes { get;}
        public List<Marble> Marbles { get; }
        public List<Move> Moves { get; }

        public void Do(Move move)
        {
            var marbleMoveDirection = Marble.OppositeDirections[move.Direction];
            var marbleOrderingComparer = new MarbleOrderingComparer(marbleMoveDirection);
            Marbles.Sort(marbleOrderingComparer);
            for (int i = 0; i < Marbles.Count; i++)
            {
                var marble = Marbles[i];
                var squiresToVisit = GetSquiresForMarbleMoveByDirection(marble, marbleMoveDirection);
                var currentSquire = squiresToVisit[0];
                squiresToVisit.Remove(currentSquire);
                if (currentSquire.HasWall(marbleMoveDirection)) continue;

                foreach (var nextSquire in squiresToVisit)
                {
                    if (currentSquire.HasWall(marbleMoveDirection)) break;
                    if (nextSquire.HasMarble
                        && nextSquire.Marble.Number != marble.Number) break;

                    var nextLocation = marble.NextLocation(marbleMoveDirection);

                    if (nextSquire.HasEmptyHole
                        && nextSquire.Hole.CanLocateMarbleAt(nextLocation)
                        && nextSquire.Hole.HasSameNumberAs(marble.Number))
                    {
                        marble.Location = nextLocation;
                        currentSquire.Marble = null;
                        nextSquire.Hole.Marble = marble;
                        Marbles.Remove(marble);
                        i--;
                        move.IsInRightDirection = true;
                        break;
                    }
                    else if (nextSquire.CanLocateMarbleAt(nextLocation))
                    {
                        marble.Location = nextLocation;
                        currentSquire.Marble = null;
                        nextSquire.Marble = marble;
                        currentSquire = nextSquire;
                    }
                }
            }
            Moves.Add(move);
        }

        private List<SquireUnit> GetSquiresForMarbleMoveByDirection(Marble marble, MoveDirection marbleDirection)
        {
            var (from, to) = (0, 0);
            var squires = new List<SquireUnit>();
            switch (marbleDirection)
            {
                case MoveDirection.N:
                    (from, to) = (marble.Location.Row, 0);
                    for (int i = from; i >= to; i--)
                    {
                        squires.Add(SquireUnits[i, marble.Location.Column]);
                    }
                    break;
                case MoveDirection.S:
                    (from, to) = (marble.Location.Row, Size);
                    for (int i = from; i < to; i++)
                    {
                        squires.Add(SquireUnits[i, marble.Location.Column]);
                    }
                    break;
                case MoveDirection.E:
                    (from, to) = (marble.Location.Column, Size);
                    for (int i = from; i < to; i++)
                    {
                        squires.Add(SquireUnits[marble.Location.Row, i]);
                    }
                    break;
                case MoveDirection.W:
                    (from, to) = (marble.Location.Column, 0);
                    for (int i = from; i >= to; i--)
                    {
                        squires.Add(SquireUnits[marble.Location.Row, i]);
                    }
                    break;
                default: break;
            }
            return squires;
        }

        public void SetHolesForSquires()
        {
            for (var index = 0; index < Holes.Count; index++)
            {
                var hole = Holes[index];
                var (i, j) = (hole.Location.Row, hole.Location.Column);
                if (SquireUnits[i, j].TryLocateHole(hole) == false)
                {
                    throw new Exception("Could not locate a hole on squire-unit");
                }

                var marble = Marbles[index];
                (i, j) = (marble.Location.Row, marble.Location.Column);
                if (SquireUnits[i, j].CanLocateMarbleAt(marble.Location))
                {
                    SquireUnits[i, j].Marble = marble;
                }
                else
                {
                    throw new Exception("Could not locate a marble on squire-unit");
                }

                if (hole.CanLocateMarbleAt(marble.Location) == true)
                {
                    throw new Exception("Marble can not be located in hole at the start of game");
                }
            }
        }

        private void SetWallsForSquires(List<Wall> walls)
        {
            // Set input Walls for squires
            foreach (var wall in walls)
            {
                var (i, j) = (wall.Location1.Row, wall.Location1.Column);
                SquireUnits[i, j].Walls.Add(wall);

                (i, j) = (wall.Location2.Row, wall.Location2.Column);
                SquireUnits[i, j].Walls.Add(wall);
            }
            // Set board edges as walls for squires
            var size = SquireUnits.GetLength(0);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if (i == 0)
                        SquireUnits[i, j].Walls.Add(new Wall(new Location(-1, j), new Location(i, j)));
                    if (j == 0)
                        SquireUnits[i, j].Walls.Add(new Wall(new Location(i, -1), new Location(i, j)));
                    if (i == size - 1)
                        SquireUnits[i, j].Walls.Add(new Wall(new Location(i, j), new Location(-10, j)));
                    if (j == size - 1)
                        SquireUnits[i, j].Walls.Add(new Wall(new Location(i, j), new Location(i, -10)));
                }
        }
    }
}