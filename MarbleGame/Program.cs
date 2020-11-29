using System;
using System.Collections.Generic;
using System.Linq;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            var testCaseInputs = ParseTestCaseInputDataFromFile("./test-cases.txt");
            for (int i = 0; i < testCaseInputs.Count; i++)
            {
                var game = new MarbleGame(i + 1, testCaseInputs[i]);
                game.Start();
                Console.WriteLine(game.Result);
            }
        }

        public static List<TestCaseInputDataModel> ParseTestCaseInputDataFromFile(string fileName = "./input.txt")
        {
            var testCaseDataModels = new List<TestCaseInputDataModel>();
            var inputText = System.IO.File.ReadAllText(fileName);
            var inputDataLines = Array.ConvertAll(inputText.Split('\n'), item => item);
            TestCaseInputDataModel testCaseData = null;
            foreach (var line in inputDataLines)
            {
                var inputDataLine = Array.ConvertAll(line.Split(' '), item => Convert.ToInt32(item));
                // The input file ends with a line containing 3 zeros
                if (inputDataLine.Length == 3
                    && inputDataLine.All(item => item == 0))
                {
                    break;
                }
                //first line of each test case contains 3 numbers: N M W
                else if (inputDataLine.Length == 3)
                {
                    testCaseData = new TestCaseInputDataModel
                    {
                        Marbles = new List<Marble>(),
                        Holes = new List<Hole>(),
                        Walls = new List<Wall>()
                    };
                    testCaseDataModels.Add(testCaseData);

                    // first the size N [2, 40] of the board
                    var size = testCaseData.BoardSize = inputDataLine[0];
                    if (size < 2 || size > 40)
                        throw new Exception("Game-board size should be in range of [2, 40]");
                    // init squire-units
                    testCaseData.SquireUnits = new SquireUnit[size, size];
                    for (int i = 0; i < size; i++)
                        for (int j = 0; j < size; j++)
                        {
                            var squireUnitLocation = new Location(i, j);
                            var squireUnit = new SquireUnit(squireUnitLocation);

                            testCaseData.SquireUnits[i, j] = squireUnit;
                        }

                    // second the number M (M > 0) of marbles
                    testCaseData.MarbleOrHoleCount = inputDataLine[1];
                    if (testCaseData.MarbleOrHoleCount <= 0)
                        throw new Exception("Marble or hole count should be positive integer");

                    // third the number W of walls
                    testCaseData.WallCount = inputDataLine[2];
                    if (testCaseData.WallCount < 0)
                        throw new Exception("Wall count can not be negative");

                    continue;
                }
                // next M lines contains 2 integers: location(row, column)
                else if (inputDataLine.Length == 2)
                {
                    if (testCaseData.Marbles.Count < testCaseData.MarbleOrHoleCount)
                    {
                        // the locations of the marbles
                        var location = new Location(
                            row: inputDataLine[0], // first integer is a row location
                            column: inputDataLine[1] // the second is a column location
                        );
                        // Marbles are numbered from 1 to M
                        var marbleNumber = testCaseData.Marbles.Count + 1;
                        var marble = new Marble(marbleNumber, location);
                        testCaseData.Marbles.Add(marble);
                        continue;
                    }
                    // next M of those lines represent the locations of the holes
                    else if (testCaseData.Holes.Count < testCaseData.MarbleOrHoleCount)
                    {
                        // the locations of holes
                        var location = new Location(
                            row: inputDataLine[0], // first integer is a row location
                            column: inputDataLine[1] // the second is a column location
                        );
                        // Holes are numbered from 1 to M
                        var holeNumber = testCaseData.Holes.Count + 1;
                        var hole = new Hole(holeNumber, location);
                        testCaseData.Holes.Add(hole);
                        continue;
                    }
                }
                // next W lines represent the wall locations, each with 4 integers
                else if (inputDataLine.Length == 4)
                {
                    if (testCaseData.Walls.Count < testCaseData.WallCount)
                    {
                        // first pair are the (row, column) of the square on one side of the wall
                        var location1 = new Location(
                            row: inputDataLine[0],
                            column: inputDataLine[1]
                        );
                        // he second pair are the (row, column) of the square on the other side of the wall
                        var location2 = new Location(
                            row: inputDataLine[2],
                            column: inputDataLine[3] // the second is a column location
                        );
                        if ((location1.Row > location2.Row && location1.Column == location2.Column)
                            || (location1.Row == location2.Row && location1.Column > location2.Column))
                        {
                            var tmp = location1;
                            location1 = location2;
                            location2 = tmp;
                        }
                        var wall = new Wall(location1, location2);
                        testCaseData.Walls.Add(wall);
                        continue;
                    }
                }
            }
            return testCaseDataModels;
        }
    }
}
