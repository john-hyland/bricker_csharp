using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Bricker
{
    /// <summary>
    /// Represents the 10x20 game matrix, game variables, and some game logic.
    /// </summary>
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
        private double[] _levelDropIntervals;

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

            _levelDropIntervals = new double[10];
            double interval = 2.0d;
            for (int i = 0; i < 10; i++)
            {
                interval *= 0.8;
                _levelDropIntervals[i] = interval;
            }

            NewGame();
        }

        /// <summary>
        /// Resets game matrix, stats, etc.
        /// </summary>
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

        /// <summary>
        /// Spawns a new random brick at the top of game matrix.
        /// </summary>
        public bool SpawnBrick()
        {
            int shapeNum;
            if (_nextBrick == null)
            {
                shapeNum = _random.Next(7) + 1;
                _nextBrick = new Brick(shapeNum);
            }
            _brick = _nextBrick;
            shapeNum = _random.Next(7) + 1;
            _nextBrick = new Brick(shapeNum);
            return _nextBrick.Collision(_matrix);
        }

        /// <summary>
        /// Moves a live brick to the static matrix, once it's come to rest.
        /// </summary>
        public void AddBrickToMatrix()
        {
            if (_brick != null)
            {
                for (int x = 0; x < _brick.Width; x++)
                {
                    for (int y = 0; y < _brick.Height; y++)
                    {
                        if (_brick.Grid[x, y] == 1)
                        {
                            _matrix[x + _brick.X, y + _brick.Y] = 1;
                            _color[x + _brick.X, y + _brick.Y] = _brick.Color;
                        }
                    }
                }
            }
            _brick = null;
        }

        /// <summary>
        /// Moves the live brick left, if one exists.
        /// </summary>
        public void MoveBrickLeft()
        {
            if (_brick != null)
                _brick.MoveLeft(_matrix);
        }

        /// <summary>
        /// Moves the live brick right, if one exists.
        /// </summary>
        public void MoveBrickRight()
        {
            if (_brick != null)
                _brick.MoveRight(_matrix);
        }

        /// <summary>
        /// Moves the live brick down, if one exists.
        /// </summary>
        public bool MoveBrickDown()
        {
            bool hit = false;
            if (_brick != null)
                hit = _brick.MoveDown(_matrix);
            if (hit)
                _stats.IncrementScore(1);
            return hit;
        }

        /// <summary>
        /// Rotates the live brick, if one exists.
        /// </summary>
        public void RotateBrick()
        {
            if (_brick != null)
                _brick.Rotate(_matrix);
        }

        /// <summary>
        /// Executed when brick hits bottom and comes to rest.
        /// </summary>
        public bool BrickHit()
        {
            /*
            self.add_brick_to_matrix()
            rows_to_erase = self.identify_solid_rows()
            if len(rows_to_erase) > 0:
                rows = len(rows_to_erase)
                self.stats.add_lines(rows)
                points = 40
                if rows == 2:
                    points = 100
                elif rows == 3:
                    points = 300
                elif rows == 4:
                    points = 1200
                self.stats.current_score += points
                self.erase_filled_rows(rows_to_erase)
                self.drop_grid()
                self.draw.event_pump()
                self.draw.update_frame(self, None)
            collision = self.spawn_brick()
            return collision             
            */
            AddBrickToMatrix();
            List<int> rowsToErase = IdentifySolidRows();
            if (rowsToErase.Count > 0)
            {
                int rows = rowsToErase.Count;
                _stats.IncrementLines(rows);
                int points = 40;
                if (rows == 2)
                    points = 100;
                else if (rows == 3)
                    points = 300;
                else if (rows == 4)
                    points = 1200;
                _stats.IncrementScore(points);
            }

            return false;
        }

        public List<int> IdentifySolidRows()
        {
            List<int> rowsToErase = new List<int>();
            for (int y = 1; y < 21; y++)
            {
                bool solid = true;
                for (int x = 1; x < 11; x++)
                    if (_matrix[x, y] != 1)
                        solid = false;
                if (solid)
                    rowsToErase.Add(y);
            }
            return rowsToErase;
        }



    }
}