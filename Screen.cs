using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bricker
{
    public class Screen : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer _components = null;
        private Graphics _graphics = null;

        public Screen()
        {
            InitializeComponent();
            this._graphics = this.CreateGraphics();
            this.Show();
            //Pen BluePen = new Pen(Color.Blue, 3);
            //_graphics.DrawRectangle(BluePen, 0, 0, 50, 50);


            this.KeyPreview = true;
            //this.KeyPress += Screen_KeyPress;
            Main.Init(this);
        }

        public void Blit(Surface surface, int x, int y)
        {
            _graphics.DrawImage(surface.Bmp, x, y);
            //Pen BluePen = new Pen(Color.Blue, 3);
            //_graphics.DrawRectangle(BluePen, 0, 0, 50, 50);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            Main.KeyPress(e);
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    if (e.Shift)
                    {

                    }
                    else
                    {
                    }
                    break;
            }
        }

        //private void Screen_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Main.KeyPress(e);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "Form";
            this.Text = "Bricker";
            this.BackColor = GameColor.Black;
            this.ResumeLayout(false);
        }
    }
}
