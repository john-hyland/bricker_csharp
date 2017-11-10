using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bricker
{
    public class Matrix
    {
        //private
        private Draw _draw;
        private int _width;
        private int _height;
        private int[,] _matrix;
        private Color[,] _color;
        private GameStats _stats;
        private Brick _brick;
        private Brick _nextBrick;
        private Random _random;

        //public
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public int[,] Matrixx { get { return _matrix; } }
        public Color[,] Color { get { return _color; } }
        public Brick Brick { get { return _brick; } }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public Matrix(Draw draw)
        {
            _draw = draw;
            _width = 12;
            _height = 22;
            _matrix = new int[_width, _height];
            _color = new Color[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                _matrix[x, 0] = 1;
                _matrix[x, _height - 1] = 1;
            }
            for (int y = 0; y < _height; y++)
            {
                _matrix[0, y] = 1;
                _matrix[_width - 1, y] = 1;
            }
            for (int x = 0; x < _width; x++)
                for (int y = 0; y < _height; y++)
                    _color[x, y] = GameColor.Black;
            _stats = new GameStats();

            _random = new Random();
            NewGame();
        }

        public void NewGame()
        {
            _brick = null;
            _nextBrick = null;
            _matrix = new int[12, 22];
            _color = new Color[12, 22];
            for (int x = 0; x < 12; x++)
            {
                _matrix[x, 0] = 1;
                _matrix[x, 21] = 1;
            }
            for (int y = 0; y < 22; y++)
            {
                _matrix[0, y] = 1;
                _matrix[11, y] = 1;
            }
            for (int x = 0; x < 12; x++)
                for (int y = 0; y < 22; y++)
                    _color[x, y] = GameColor.Black;
            _stats = new GameStats();
            SpawnBrick();
        }

        private bool SpawnBrick()
        {
            int shapeNum;
            if (_nextBrick == null)
            {
                shapeNum = _random.Next(7) + 1;
                shapeNum = 1;
                _nextBrick = new Brick(shapeNum);
            }
            _brick = _nextBrick;
            shapeNum = _random.Next(7) + 1;
            shapeNum = 1;
            _nextBrick = new Brick(shapeNum);
            return _nextBrick.Collision(_matrix);
        }

        public void AddBrickToMatrix()
        {
            if (_brick != null)
            {
                for (int x = 0; x < _brick.Width; x++)
                {
                    for (int y = 0; y < _brick.Height; y++)
                    {
                        _matrix[x + _brick.X, y + _brick.Y] = 1;
                        _color[x + _brick.X, y + _brick.Y] = _brick.Color;
                    }
                }
            }
            _brick = null;
        }

        public void MoveBrickLeft()
        {
            if (_brick != null)
                _brick.MoveLeft(_matrix);
        }

        public void MoveBrickRight()
        {
            if (_brick != null)
                _brick.MoveRight(_matrix);
        }

        public bool MoveBrickDown()
        {
            bool hit = false;
            if (_brick != null)
                hit = _brick.MoveDown(_matrix);
            if (hit)
                _stats.IncrementScore(1);
            return hit;
        }

        public void RotateBrick()
        {
            if (_brick != null)
                _brick.Rotate(_matrix);
        }

    }
}