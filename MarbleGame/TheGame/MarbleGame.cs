using System;
using System.Collections.Generic;
using System.Linq;

namespace game
{
    public class MarbleGame
    {
        private TestCaseInputDataModel TestCaseInput;
        public MarbleGame(int caseNumber, TestCaseInputDataModel testCaseInput)
        {
            CaseNumber = caseNumber;
            TestCaseInput = testCaseInput;
            Board = new SquireBoard(CopyInputData(TestCaseInput));
        }
        public SquireBoard Board { get; private set; }
        public int CaseNumber { get; }
        public string Result
        {
            get
            {
                return Board.Moves.Count > 0
                    ? $"Case {CaseNumber}: {Board.Moves.Count} moves {string.Join("", Board.Moves.Select(m => m.Name))}"
                    : $"Case {CaseNumber}: impossible";
            }
        }
        public void Start()
        {
            // ENSWN
            //{ East, North, South, West, North };
            var combinations = new Dictionary<string, List<Move>>();

            var group1 = Permuts(Directions)
                .Select(p => p.Select(d => new Move(d)).ToList())
                .ToList();

            foreach (var moves1 in group1)
                foreach (var moves2 in group1)
                {
                    var moves = moves1.ToList();
                    moves.AddRange(moves2);

                    foreach (var move in moves)
                    {
                        Board.Do(move);
                        if (!Board.Holes.Any(h => h.IsEmpty))
                        {
                            var solutions = Board.Moves.ToList();
                            var key = string.Join("", solutions.Select(m => m.Direction));

                            if (solutions.Count > 0
                                && (!combinations.ContainsKey(key)
                                || combinations[key].Count > solutions.Count))
                            {
                                combinations[key] = solutions;
                            }
                            break;
                        }
                    }
                    Board = new SquireBoard(CopyInputData(TestCaseInput));
                }
            Board.Moves.Clear();
            if (combinations.Count > 0)
                Board.Moves.AddRange(combinations.Values.OrderBy(v => v.Count).First());
        }

        private TestCaseInputDataModel CopyInputData(TestCaseInputDataModel testCaseData)
        {
            var size = testCaseData.BoardSize;
            var squireUnits = new SquireUnit[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    var squireUnitLocation = new Location(i, j);
                    var squireUnit = new SquireUnit(squireUnitLocation);

                    squireUnits[i, j] = squireUnit;
                }
            return new TestCaseInputDataModel
            {
                BoardSize = size,
                MarbleOrHoleCount = testCaseData.MarbleOrHoleCount,
                WallCount = testCaseData.WallCount,
                SquireUnits = squireUnits,
                Marbles = testCaseData.Marbles.Select(e => e.Copy()).ToList(),
                Holes = testCaseData.Holes.Select(e => e.Copy()).ToList(),
                Walls = testCaseData.Walls.Select(e => e.Copy()).ToList()
            };
        }
        public List<MoveDirection> Directions = new List<MoveDirection> {
            MoveDirection.N,
            MoveDirection.S,
            MoveDirection.E,
            MoveDirection.W
        };
        private List<List<MoveDirection>> Permuts(List<MoveDirection> source)
        {
            if (source.Count == 1) return new List<List<MoveDirection>> { source };

            var permutations = from c in source
                               from p in Permuts(source.Where(x => x != c).ToList())
                               select new List<MoveDirection>(p) { c };

            return permutations.ToList();
        }

    }
}