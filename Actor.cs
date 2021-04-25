
using System;
using System.IO;

namespace monster

{

    class Actor {

        private GameCanvas _canvasRef;
        private int _row = 0;
        private int _col = 0;

        public char Graphic;

        public bool IsVisible;
  
        public int X {
            get => _col;
        }

        public int Y {
            get => _row;
        }

        public Actor(GameCanvas cavasRef, char graphic, bool isVisible) {
            _canvasRef = cavasRef;
            Graphic = graphic;
            IsVisible = isVisible;
        }

        public void MoveTo((int x, int y) coords) {
            if (!_canvasRef.IsPositionValid(coords.x, coords.y)) throw new Exception();
            int prevCol = _col;
            int prevRow = _row;

            _col = coords.x;
            _row = coords.y; 

            if (IsVisible) {  
                _canvasRef.Grid[prevRow, prevCol] = ' ';
                _canvasRef.Grid[_row, _col] = Graphic;
            }
        }
        public void SetX(int x) => MoveTo((x, _row));
        public void SetY(int y) => MoveTo((_col, y));

        public bool DoesCollideWith(Actor actor) => (X == actor.X) && (Y == actor.Y); 
        public bool DoesCollideMultiple(Actor[] actors) {
            foreach(Actor actor in actors) {
                if (DoesCollideWith(actor)) return true;
            }
            return false;
        }

    }
        
}

