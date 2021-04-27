
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace monster

{
    // class Grid {
    //     private char[,] arr;
    //     public int Rows;
    //     public int Cols;
    //     public List<int> occupiedIndexes = new List<int>();

    //     public char this[int row, int col] {
    //         get => arr[row, col];
    //         set {
    //             if (value != ' ') occupiedIndexes.Add((Cols * row) + col);
    //             arr[row,col] = value;
    //         }
    //     }
    //     public Grid(int rows, int cols) {
    //         Rows = rows;
    //         Cols = cols;
    //         arr = new char[Rows, Cols];
    //     }
    // }

    class GameCanvas {

       public char[,] Grid;

        private int _rows;
        private int _cols;
        public int Width
        {
            get => _cols;
        }

        public int Height
        {
            get => _rows;
        }


        private static Random _rnd = new Random();

        public GameCanvas(int width, int height)
        {
            _rows = height;
            _cols = width;
            Grid = new char[height, width];
            Reset();
        }

        public void Reset()
        {
            for (int Row = 0; Row < _rows; Row++)
            {
                for (int Col = 0; Col < _cols; Col++)
                {
                    Grid[Row, Col] = ' ';
                }
            }
        }

        public Actor CreateActor(char graphic, bool isVisible = true)
        {
            return new Actor(this, graphic, isVisible);
        }

        public bool IsPositionValid(int x, int y) => !( (x == _cols || x < 0) || (y == _rows || y < 0) );

        public bool DoActorsCollide(Actor actor1, Actor actor2) => (actor1.X == actor2.X) && (actor1.Y == actor2.Y);

        public void Draw()
        {

            for (int Row = 0; Row < _rows; Row++)
            {
                Console.WriteLine(new String('-', (2 * _cols) + 1));
                for (int Col = 0; Col < _cols; Col++)
                {
                    Console.Write("|" + Grid[Row, Col]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(new String('-', (2 * _cols) + 1));
            Console.WriteLine();
        }


        public (int x, int y) GetRandomPosition()
        {
            int x = _rnd.Next(0, _cols - 1);
            int y = _rnd.Next(0, _rows - 1);
            return (x, y);
        }

    }        
}
