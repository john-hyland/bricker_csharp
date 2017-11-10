using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Bricker
{
    public class Surface : IDisposable
    {
        private int _width;
        private int _height;
        private Bitmap _bmp;
        private Graphics _gfx;

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public Bitmap Bmp { get { return _bmp; } }

        public Surface(int width, int height)
        {
            _width = width;
            _width = height;
            _bmp = new Bitmap(_width, _height);
            _gfx = Graphics.FromImage(_bmp);
        }

        public Surface(int width, int height, Color color)
        {
            _width = width;
            _height = height;
            _bmp = new Bitmap(width, height);
            _gfx = Graphics.FromImage(_bmp);

            Fill(color);
        }

        public void Fill(Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                _gfx.FillRectangle(brush, 0, 0, _width, _height);
            }
        }

        public void DrawLine(Color color, int startX, int startY, int endX, int endY, float width = 1.0f)
        {
            using (Pen pen = new Pen(color, width))
            {
                _gfx.DrawLine(pen, startX, startY, endX, endY);
            }
        }

        public void DrawRectangle(Color color, int x, int y, int width, int height)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                _gfx.FillRectangle(brush, x, y, width, height);
            }
        }

        public void Blit(Surface surface, int x, int y)
        {
            _gfx.DrawImage(surface.Bmp, x, y);
        }

        public void Dispose()
        {
            if (_gfx != null)
                _gfx.Dispose();
        }






    }
}
