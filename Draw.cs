using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Bricker
{
    public class Draw
    {
        private Screen _screen;

        public Draw(Screen screen)
        {
            _screen = screen;
        }

        public void DrawFrame(Matrix matrix)
        {
            //vars
            int sideWidth = (1000 - 333) / 2;
            int leftX = ((sideWidth - 250) / 2) + 5;
            int rightX = sideWidth + 333 + leftX;

            //create new frame
            Surface frame = new Surface(1000, 700, GameColor.Black);

            //game matrix
            Surface matrixSurface = DrawMatrix(matrix);
            frame.Blit(matrixSurface, sideWidth, (700 - 663) / 2);

            _screen.Blit(frame, 0, 0);
        }

        public Surface DrawBlankGrid()
        {
            Surface grid = new Surface(333, 663, GameColor.Black);

            grid.DrawLine(GameColor.White, 0, 0, 332, 0);
            grid.DrawLine(GameColor.White, 0, 1, 332, 1);
            grid.DrawLine(GameColor.White, 0, 0, 0, 662);
            grid.DrawLine(GameColor.White, 1, 0, 1, 662);
            grid.DrawLine(GameColor.White, 0, 662, 332, 662);
            grid.DrawLine(GameColor.White, 0, 661, 332, 661);
            grid.DrawLine(GameColor.White, 332, 0, 332, 662);
            grid.DrawLine(GameColor.White, 331, 0, 331, 662);

            for (int i = 1; i < 10; i++)
            {
                int x = (i * 33) + 1;
                grid.DrawLine(GameColor.Gray, x, 2, x, 660);
            }

            for (int i = 1; i < 20; i++)
            {
                int y = (i * 33) + 1;
                grid.DrawLine(GameColor.Gray, 2, y, 330, y);
            }

            return grid;
        }

        public Surface DrawMatrix(Matrix matrix)
        {
            Surface matrixSurface = DrawBlankGrid();

            for (int x = 1; x < (matrix.Width - 1); x++)
            {
                for (int y = 1; y < (matrix.Height - 1); y++)
                {
                    Color color = matrix.Color[x, y];
                    if (color != Color.Black)
                        matrixSurface.DrawRectangle(color, ((x - 1) * 33) + 2, ((y - 1) * 33) + 2, 32, 32);
                }
            }

            if (matrix.Brick != null)
            {
                for (int x = 0; x < matrix.Brick.Width; x++)
                {
                    for (int y = 0; y < matrix.Brick.Height; y++)
                    {
                        if (matrix.Brick.Grid[x, y] == 1)
                            matrixSurface.DrawRectangle(matrix.Brick.Color, (((matrix.Brick.X - 1) + x) * 33) + 2, (((matrix.Brick.Y - 1) + y) * 33) + 2, 32, 32);
                    }
                }
            }

            return matrixSurface;
        }


    }
}
