using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Chess.Figures;

namespace Chess
{
    public partial class PlayWindow : Form
    {
        public static GuiMatrix matrix;
        private System.Windows.Forms.Timer mouseTracker;
        private int sqSize { get; set; }   // размер квадрата
        private int offset { get; set; }   // отступ от края формы
        private BufferedGraphics graph = null;
        private System.Drawing.Pen pen = null;
        private delegate void DrawAsyncDelegate(object sender, EventArgs e);


        public PlayWindow(string title)
        {
            this.Text = title;
            InitializeComponent();
            matrix = new GuiMatrix();
            sqSize = 80;
            offset = 25;
        }

        private void PlayWindow_Shown(object sender, EventArgs e)
        {
            BufferedGraphicsContext graphContext = BufferedGraphicsManager.Current;
            graphContext.MaximumBuffer = new System.Drawing.Size(sqSize * 8 + offset + 1, sqSize * 8 + offset + 1);
            graph = graphContext.Allocate(this.CreateGraphics(), new Rectangle(0, 0, sqSize * 8 + offset + 1, sqSize * 8 + offset + 1));

            pen = new System.Drawing.Pen(Color.CadetBlue);

            mouseTracker = new System.Windows.Forms.Timer();
            mouseTracker.Interval = 40;
            mouseTracker.Tick += new EventHandler(MouseTracking);
            mouseTracker.Start();

            ReDraw(true);
        }
    
        //draw chess field
        private void Draw(object sender, EventArgs e)
        {
            Spot tmpSpot;
            pen.Color = this.BackColor;
            graph.Graphics.FillRectangle(pen.Brush, 0, 0, sqSize * 8 + offset + 1, sqSize * 8 + offset + 1);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tmpSpot = matrix.GetSpot(i, j);

                    //Выбор цвета квадрата
                    if (!tmpSpot.Highlighted && !tmpSpot.Selected) pen.Color = tmpSpot.sColor;
                    else
                    {
                        if (tmpSpot.Highlighted)
                        {
                            if (tmpSpot.sColor == Color.CadetBlue) pen.Color = Color.FromArgb(Color.CadetBlue.R - 40, Color.CadetBlue.G - 40, Color.CadetBlue.B - 40);
                            else pen.Color = Color.FromArgb(Color.CadetBlue.R + 40, Color.CadetBlue.G + 40, Color.CadetBlue.B + 40);
                        }
                        if (tmpSpot.Selected) pen.Color = Color.PaleGoldenrod;
                    }

                    //отрисовка квадрата
                    if (tmpSpot.Focused)
                    {
                        pen.Color = Color.FromArgb(pen.Color.R - 20, pen.Color.G - 20, pen.Color.B - 20);
                        graph.Graphics.FillRectangle(pen.Brush, offset + sqSize * i, sqSize * j, sqSize, sqSize);
                        pen.Color = Color.FromArgb(pen.Color.R + 20, pen.Color.G + 20, pen.Color.B + 20);
                        graph.Graphics.FillRectangle(pen.Brush, offset + sqSize * i + 10, sqSize * j + 10, sqSize - 20, sqSize - 20);
                    }
                    else
                    {
                        graph.Graphics.FillRectangle(pen.Brush, offset + sqSize * i, sqSize * j, sqSize, sqSize);
                    }
                }
            }
            pen.Color = Color.Gray;
            graph.Graphics.DrawRectangle(pen, offset, 0, sqSize * 8, sqSize * 8);
            pen.Color = Color.Black;
            char ch = 'A';
            for (int i = 0; i < 8; i++)
            {//вывод букв
                graph.Graphics.DrawString(Convert.ToString(ch), this.Font, pen.Brush, offset - 4 + sqSize / 2 + sqSize * i, sqSize * 8 + 5);
                ch++;
            }
            for (int i = 0; i < 8; i++)
            {//вывод цифр
                graph.Graphics.DrawString(Convert.ToString((i - 8) * (-1)), this.Font, pen.Brush, 8, sqSize / 2 - 4 + sqSize * i);
                ch++;
            }
            
            //Image img = new Bitmap("C:\\tmp\\lol.png");
            //DrawFigure(new Position(4, 3), img);
            graph.Render();
        }

        private void DrawFigure(Position p, System.Drawing.Image img)
        {
            graph.Graphics.DrawImage(img, p.X * sqSize + offset, p.Y * sqSize);
        }

        private void ReDraw()
        {
            Draw(new object(), new EventArgs());
        }

        private void ReDraw(bool async)
        {
            if (async)
            {
                DrawAsyncDelegate d = new DrawAsyncDelegate(Draw);
                d.BeginInvoke(new object(), new EventArgs(), null, null);
            }
            else Draw(new object(), new EventArgs());
        }

        private void MouseTracking(object sender, EventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            pt.X -= offset;
            pt.X /= sqSize;
            pt.Y /= sqSize;
            if (pt.X >= 8 || pt.Y >= 8 || pt.X < 0 || pt.Y < 0) return;
            if (matrix.MakeFocused(pt.X, pt.Y)) ReDraw(true);
        }

        private void PlayWindow_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            pt.X -= offset;
            pt.X /= sqSize;
            pt.Y /= sqSize;
            if (pt.X >= 8 || pt.Y >= 8 || pt.X < 0 || pt.Y < 0) return;
            if (matrix.MakeSelected(pt.X, pt.Y)) ReDraw(true);
        }
        
        private void PlayWindow_Move(object sender, EventArgs e)
        {
            if (graph != null) ReDraw(true);
        }

        private void PlayWindow_Deactivate(object sender, EventArgs e)
        {
            if (mouseTracker != null) mouseTracker.Stop();
            if(graph != null) ReDraw(true);
        }

        private void PlayWindow_Activated(object sender, EventArgs e)
        {
            if (mouseTracker != null && !mouseTracker.Enabled) mouseTracker.Start();
            if (graph != null) ReDraw(true);
        }
    }
}
