using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bricker
{
    /// <summary>
    /// The game mode, determines action in event loop.
    /// </summary>
    public enum GameMode
    {
        Menu,
        Game,
        Score
    }
    
    /// <summary>
    /// The main program class, scoped static for easy access.
    /// </summary>
    public static class Main
    {
        //private
        private static GameMode _mode;
        private static Screen _form;
        private static Draw _draw;
        private static Matrix _matrix;
        private static Queue<KeyEventArgs> _keyQueue;
        private static Timer _timer;

        //public
        public static Screen Form { get { return _form; } }

        /// <summary>
        /// Initializes main class.
        /// </summary>
        public static void Init(Screen form)
        {
            _mode = GameMode.Game;
            _form = form;
            _draw = new Draw(_form);
            _matrix = new Matrix(_draw);
            _keyQueue = new Queue<KeyEventArgs>();

            _timer = new Timer();
            _timer.Tick += TimerTick;
            _timer.Interval = 33;
            _timer.Start();

            //_draw.DrawFrame(_matrix);
        }

        /// <summary>
        /// Simulated event loop using a timer.
        /// </summary>
        private static void TimerTick(object sender, EventArgs ea)
        {
            if (_mode == GameMode.Game)
            {
                bool hit = false;
                while (_keyQueue.Count > 0)
                {
                    KeyEventArgs kea = _keyQueue.Dequeue();
                    switch (kea.KeyCode)
                    {
                        case Keys.Left:
                            _matrix.MoveBrickLeft();
                            break;
                        case Keys.Right:
                            _matrix.MoveBrickRight();
                            break;
                        case Keys.Down:
                            _matrix.MoveBrickDown();
                            break;
                        case Keys.Up:
                            _matrix.RotateBrick();
                            break;
                        case Keys.Space:
                            if (_matrix.Brick != null)
                            {
                                hit = false;
                                while (!hit)
                                {
                                    for (int i = 0; i < 3; i++)
                                        if (!hit)
                                            hit = _matrix.MoveBrickDown();
                                    _draw.RefreshFrame(_matrix);
                                    System.Threading.Thread.Sleep(15);
                                }
                                _matrix.AddBrickToMatrix();
                                _matrix.SpawnBrick();
                            }
                            break;
                    }
                }

                _draw.RefreshFrame(_matrix);
            }
        }

        public static void KeyPress(KeyEventArgs e)
        {
            _keyQueue.Enqueue(e);
        }








    }
}
