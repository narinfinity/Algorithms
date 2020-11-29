using System;
using System.Collections.Generic;
using System.Linq;

namespace game
{
    public class TestCaseInputDataModel
    {
        public int BoardSize { get; set; }
        public int MarbleOrHoleCount { get; set; }
        public int WallCount { get; set; }
        public SquireUnit[,] SquireUnits { get; set; }
        public List<Marble> Marbles { get;  set; }
        public List<Hole> Holes { get; set; }
        public List<Wall> Walls { get; set; }
    }
}