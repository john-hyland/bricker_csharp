using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bricker
{
    public class Brick
    {
        //private
        private int _shapeNum;
        private int _width;
        private int _height;
        private int[,] _grid;
        private Color _color;
        private int _topSpace;
        private int _bottomSpace;
        private int _x;
        private int _y;
        private long _lastDropTime;

        //public
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public int[,] Grid { get { return _grid; } }
        public Color Color { get { return _color; } }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public Brick(int shapeNum)
        {
            _shapeNum = shapeNum;
            switch (shapeNum)
            {
                case 1:
                    _width = 4;
                    _height = 4;
                    _grid = new int[_width, _height];
                    _grid[0, 2] = 1;
                    _grid[1, 2] = 1;
                    _grid[2, 2] = 1;
                    _grid[3, 2] = 1;
                    _color = GameColor.SilverPink;
                    break;
                case 2:
                    _width = 3;
                    _height = 3;
                    _grid = new int[_width, _height];
                    _grid[0, 1] = 1;
                    _grid[0, 2] = 1;
                    _grid[1, 2] = 1;
                    _grid[2, 2] = 1;
                    _color = GameColor.TuftsBlue;
                    break;
                case 3:
                    _width = 3;
                    _height = 3;
                    _grid = new int[_width, _height];
                    _grid[2, 1] = 1;
                    _grid[0, 2] = 1;
                    _grid[1, 2] = 1;
                    _grid[2, 2] = 1;
                    _color = GameColor.ChromeYellow;
                    break;
                case 4:
                    _width = 2;
                    _height = 2;
                    _grid = new int[_width, _height];
                    _grid[0, 0] = 1;
                    _grid[0, 1] = 1;
                    _grid[1, 0] = 1;
                    _grid[1, 1] = 1;
                    _color = GameColor.Independence;
                    break;
                case 5:
                    _width = 4;
                    _height = 4;
                    _grid = new int[_width, _height];
                    _grid[1, 0] = 1;
                    _grid[2, 0] = 1;
                    _grid[0, 1] = 1;
                    _grid[1, 1] = 1;
                    _color = GameColor.ForestGreen;
                    break;
                case 6:
                    _width = 4;
                    _height = 4;
                    _grid = new int[_width, _height];
                    _grid[1, 1] = 1;
                    _grid[0, 2] = 1;
                    _grid[1, 2] = 1;
                    _grid[2, 2] = 1;
                    _color = GameColor.Byzantine;
                    break;
                case 7:
                    _width = 4;
                    _height = 4;
                    _grid = new int[_width, _height];
                    _grid[0, 0] = 1;
                    _grid[1, 0] = 1;
                    _grid[1, 1] = 1;
                    _grid[2, 1] = 1;
                    _color = GameColor.Coquelicot;
                    break;
            }
            _topSpace = getTopSpace();
            _bottomSpace = getBottomSpace();
            _x = (12 - _width) / 2;
            _y = 1 - _topSpace;
        }

        /// <summary>
        /// Gets spacing at top of brick.
        /// </summary>
        private int getTopSpace()
        {
            int topSpace = 0;
            for (int y = 0; y < _height; y++)
            {
                bool empty = true;
                for (int x = 0; x < _width; x++)
                {
                    if (_grid[x, y] == 1)
                        empty = false;
                }
                if (empty)
                    topSpace++;
                else
                    break;
            }
            return topSpace;
        }

        /// <summary>
        /// Gets spacing at bottom of brick.
        /// </summary>
        private int getBottomSpace()
        {
            int bottomSpace = 0;
            for (int y = _height - 1; y <= 0; y--)
            {
                bool empty = true;
                for (int x = 0; x < _width; x++)
                {
                    if (_grid[x, y] == 1)
                        empty = false;
                }
                if (empty)
                    bottomSpace++;
                else
                    break;
            }
            return bottomSpace;
        }

        /// <summary>
        /// Returns true if filled brick space conflicts with filled matrix space.
        /// </summary>
        public bool Collision(int[,] matrix)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    int mX = x + _x;
                    int mY = y + _y;
                    if ((mX < 0) || (mX > 21))
                        return true;
                    if ((mY < 0) || (mY > 21))
                        return true;
                    if ((_grid[x, y] == 1) && (matrix[mX, mY] == 1))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Moves the brick left, unless collision.
        /// </summary>
        public void MoveLeft(int[,] matrix)
        {
            _x--;
            if (Collision(matrix))
                _x++;
        }

        /// <summary>
        /// Moves the brick right, unless collision.
        /// </summary>
        public void MoveRight(int[,] matrix)
        {
            _x++;
            if (Collision(matrix))
                _x--;
        }

        /// <summary>
        /// Moves the brick down, unless collision.
        /// </summary>
        public bool MoveDown(int[,] matrix)
        {
            _lastDropTime = DateTime.Now.Ticks;
            _y++;
            if (Collision(matrix))
            {
                _y--;
                return true;
            }
            return false;
        }

        public void Rotate(int[,] matrix)
        {
            int[,] newGrid = new int[_height, _width];
            for (int x1 = 0; x1 < _width; x1++)
            {
                for (int y1 = 0; y1 < _height; y1++)
                {
                    int x2 = -y1 + (_height - 1);
                    int y2 = x1;
                    newGrid[x2, y2] = _grid[x1, y1];
                }
            }
            _grid = newGrid;

            int steps = 0;
            while (Collision(matrix))
            {
                _y++;
                steps++;
                if (steps >= 3)
                {
                    _y -= 3;
                    break;
                }
            }

            steps = 0;
            while (Collision(matrix))
            {
                _y--;
                steps++;
                if (steps >= 3)
                {
                    _y += 3;
                    break;
                }
            }

            steps = 0;
            while (Collision(matrix))
            {
                _x++;
                steps++;
                if (steps >= 3)
                {
                    _x -= 3;
                    break;
                }
            }

            steps = 0;
            while (Collision(matrix))
            {
                _x--;
                steps++;
                if (steps >= 3)
                {
                    _x += 3;
                    break;
                }
            }
        }






    }
}
