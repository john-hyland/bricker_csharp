using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bricker
{
    /// <summary>
    /// The main program class, scoped static for easy access.
    /// </summary>
    public static class Main
    {
        //private
        private static Screen _form;
        private static Draw _draw;
        private static Matrix _matrix;

        //public
        public static Screen Form { get { return _form; } }

        /// <summary>
        /// Initializes main class.
        /// </summary>
        public static void Init(Screen form)
        {
            _form = form;
            _draw = new Draw(_form);
            _matrix = new Matrix(_draw);

            _draw.DrawFrame(_matrix);
        }

        public static void KeyPress(KeyEventArgs e)
        {
            switch (e.KeyCode)
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
            }

            _draw.DrawFrame(_matrix);
        }








    }
}
