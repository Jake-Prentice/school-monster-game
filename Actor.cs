
using System;
using System.IO;
using System.Collections.Generic;

namespace monster

{

    class Actor {

        private GameCanvas _canvasRef;

        private int _row = 0;
        private int _col = 0;

        public char Graphic;

        public bool IsVisible;

        //events
        public delegate void OnMoveHandler(Actor sender);
        public event OnMoveHandler OnMove;
        
        public int X
        {
            get => _col;
        }

        public int Y
        {
            get => _row;
        }

        public Actor(GameCanvas cavasRef, char graphic, bool isVisible)
        {
            _canvasRef = cavasRef;
            Graphic = graphic;
            IsVisible = isVisible;
        }

        public bool DoesCollideWith(Actor actor) => (X == actor.X) && (Y == actor.Y);

        public bool DoesCollideMultiple(List<Actor> actors)
        {
            foreach (Actor actor in actors)
            {
                if (DoesCollideWith(actor)) return true;
            }
            return false;
        }

        public void MoveTo((int x, int y) coords)
        {
            //Console.WriteLine($"X: {coords.x}, Y: {coords.y}");
            //Console.WriteLine($"is valid: {_canvasRef.IsPositionValid(coords.x, coords.y)}");
            //Console.WriteLine(coords.x > _canvasRef.Width - 1);

            if (!_canvasRef.IsPositionValid(coords.x, coords.y)) throw new Exception();
           
            OnMove?.Invoke(this);

            int prevCol = _col;
            int prevRow = _row;

            _col = coords.x;
            _row = coords.y;

            if (IsVisible)
            {
                _canvasRef.Grid[prevRow, prevCol] = ' ';
                _canvasRef.Grid[_row, _col] = Graphic;
            }

        }

        public void SetX(int x) => MoveTo((x, _row));

        public void SetY(int y) => MoveTo((_col, y));

    }

        
}

