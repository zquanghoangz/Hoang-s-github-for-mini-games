using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPuzzle8
{
    public class PuzzleGame
    {
        public string Name { get; set; }
        public PuzzlePeice[] Matrix { get; set; }

        public void CreateMatrix(int peiceSize, int locX, int locY)
        {
            Matrix = new PuzzlePeice[9];
            int[] values = RandomArray();

            Matrix[0] = new PuzzlePeice { X = locX, Y = locY, Value = values[0] };
            Matrix[1] = new PuzzlePeice { X = locX, Y = locY + peiceSize, Value = values[1] };
            Matrix[2] = new PuzzlePeice { X = locX, Y = locY + peiceSize * 2, Value = values[2] };
            Matrix[3] = new PuzzlePeice { X = locX + peiceSize, Y = locY, Value = values[3] };
            Matrix[4] = new PuzzlePeice { X = locX + peiceSize, Y = locY + peiceSize, Value = values[4] };
            Matrix[5] = new PuzzlePeice { X = locX + peiceSize, Y = locY + peiceSize * 2, Value = values[5] };
            Matrix[6] = new PuzzlePeice { X = locX + peiceSize * 2, Y = locY, Value = values[6] };
            Matrix[7] = new PuzzlePeice { X = locX + peiceSize * 2, Y = locY + peiceSize, Value = values[7] };
            Matrix[8] = new PuzzlePeice { X = locX + peiceSize * 2, Y = locY + peiceSize * 2, Value = values[8] };

            Matrix = Matrix.OrderBy(x => x.Value).ToArray();
        }

        private int[] RandomArray()
        {
            Random rad = new Random();
            int[] output = new int[9];
            for (int i = 0; i < 9; i++)
            {
                while (true)
                {
                    output[i] = rad.Next(0, 9);

                    bool isBreak = true;
                    for (int j = 0; j < i; j++)
                    {
                        if (output[j] == output[i])
                        {
                            isBreak = false;
                        }
                    }
                    if (isBreak)
                        break;
                    {

                    }
                }
            }

            return output;
        }

        //For win state
        public PuzzleGame CustomMatrix(int peiceSize, int locX, int locY)
        {
            PuzzleGame winGame = new PuzzleGame { Name = "Custom" };
            winGame.Matrix = new PuzzlePeice[9];

            winGame.Matrix[0] = new PuzzlePeice { X = locX, Y = locY, Value = 0 };
            winGame.Matrix[1] = new PuzzlePeice { X = locX + peiceSize, Y = locY, Value = 1 };
            winGame.Matrix[2] = new PuzzlePeice { X = locX + peiceSize * 2, Y = locY, Value = 2 };

            winGame.Matrix[3] = new PuzzlePeice { X = locX, Y = locY + peiceSize, Value = 3 };
            winGame.Matrix[4] = new PuzzlePeice { X = locX + peiceSize, Y = locY + peiceSize, Value = 4 };
            winGame.Matrix[5] = new PuzzlePeice { X = locX + peiceSize * 2, Y = locY + peiceSize, Value = 5 };

            winGame.Matrix[6] = new PuzzlePeice { X = locX, Y = locY + peiceSize * 2, Value = 6 };
            winGame.Matrix[7] = new PuzzlePeice { X = locX + peiceSize, Y = locY + peiceSize * 2, Value = 7 };
            winGame.Matrix[8] = new PuzzlePeice { X = locX + peiceSize * 2, Y = locY + peiceSize * 2, Value = 8 };

            return winGame;
        }

        public bool IsWin(PuzzleGame puzzleGame)
        {
            for (int i = 0; i < Matrix.Length; i++)
            {
                if (Matrix[i].X != puzzleGame.Matrix[i].X || Matrix[i].Y != puzzleGame.Matrix[i].Y)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
