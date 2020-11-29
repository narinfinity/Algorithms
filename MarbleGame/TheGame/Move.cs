using System.Collections.Generic;
using System.Linq;

namespace game
{
    public class Move
    {
        public Move(MoveDirection direction)
        {
            Direction = direction;
        }
        public MoveDirection Direction { get; }
        public char Name => Direction.ToString()[0];

        public List<Marble> Marbles { get; private set; }
        public List<Hole> Holes { get; private set; }
        public SquireUnit[,] SquireUnits { get; private set; }
        public bool IsInRightDirection { get; set; }

        public void SaveStateFor((List<Marble>, List<Hole>, SquireUnit[,]) state)
        {
            Marbles = state.Item1.Select(m => m.Copy()).ToList();
            Holes = state.Item2.Select(h => h.Copy()).ToList();
            var size = state.Item3.GetLength(0);
            SquireUnits = new SquireUnit[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    var squireUnitLocation = new Location(i, j);
                    var squireUnit = new SquireUnit(squireUnitLocation);

                    SquireUnits[i, j] = squireUnit;
                }
        }
    }
}