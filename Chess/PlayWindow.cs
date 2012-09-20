using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Chess
{
    public partial class PlayWindow : Form
    {
        static GameCore core; // Перенести                                      !

        System.Windows.Forms.Timer mouseTracker;

        private int sqSize { get; set; }   // размер квадрата
        private int offset { get; set; }   // отступ от края формы
        Graphics graph = null;
        System.Drawing.Pen pen = null;
        delegate void ReDrawAsyncDelegate(int delay);


        public PlayWindow()
        {
            InitializeComponent();
            core = new GameCore(); // перенести                                 !
            sqSize = 80;
            offset = 25;
        }
        private void PlayWindow_Shown(object sender, EventArgs e)
        {
            graph = this.CreateGraphics();
            pen = new System.Drawing.Pen(Color.CadetBlue);

            mouseTracker = new System.Windows.Forms.Timer();
            mouseTracker.Interval = 40;
            mouseTracker.Tick += new EventHandler(MouseTracking);
            mouseTracker.Start();

            ReDrawAsyncDelegate dlgt = new ReDrawAsyncDelegate(ReDrawAsync);
            dlgt.BeginInvoke(100, null, null);
        }
    

        //draw chess field
        private void Draw(object sender, EventArgs e)
        {
            Spot tmpSpot;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tmpSpot = core.GetSpot(i, j);

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
                        graph.FillRectangle(pen.Brush, offset + sqSize * i, sqSize * j, sqSize, sqSize);
                        pen.Color = Color.FromArgb(pen.Color.R + 20, pen.Color.G + 20, pen.Color.B + 20);
                        graph.FillRectangle(pen.Brush, offset + sqSize * i + 10, sqSize * j + 10, sqSize - 20, sqSize - 20);
                    }
                    else
                    {
                        graph.FillRectangle(pen.Brush, offset + sqSize * i, sqSize * j, sqSize, sqSize);
                    }
                }
            }
            pen.Color = Color.Black;
            graph.DrawRectangle(pen, offset - 1, -1, sqSize * 8 + 1, sqSize * 8 + 1);
            char ch = 'A';
            for (int i = 0; i < 8; i++)
            {//вывод букв
                graph.DrawString(Convert.ToString(ch), this.Font, pen.Brush, offset - 4 + sqSize / 2 + sqSize * i, sqSize * 8 + 5);
                ch++;
            }
            for (int i = 0; i < 8; i++)
            {//вывод цифр
                graph.DrawString(Convert.ToString((i - 8) * (-1)), this.Font, pen.Brush, 8, sqSize / 2 - 4 + sqSize * i);
                ch++;
            }
            Image img = new Bitmap("C:\\tmp\\lol.png");
            DrawFigure(new Point(6, 3), img);
        }

        private void ReDraw()
        {
            Draw(new object(), new EventArgs());
        }
        private void ReDrawAsync(int delay)
        {
            Thread.Sleep(delay);
            Draw(new object(), new EventArgs());
        }

        private void MouseTracking(object sender, EventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            pt.X -= offset;
            pt.X /= sqSize;
            pt.Y /= sqSize;
            if (pt.X >= 8 || pt.Y >= 8 || pt.X < 0 || pt.Y < 0) return;
            if (core.MakeFocused(pt.X, pt.Y)) ReDraw();
        }

        private void PlayWindow_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            pt.X -= offset;
            pt.X /= sqSize;
            pt.Y /= sqSize;
            if (pt.X >= 8 || pt.Y >= 8 || pt.X < 0 || pt.Y < 0) return;
            if (core.MakeSelected(pt.X, pt.Y)) ReDraw();
        }

        private void SelectSq(Point pt, Color clr)
        {
            pen.Color = clr;
            graph.FillRectangle(pen.Brush, offset + sqSize * pt.X, sqSize * pt.Y, sqSize, sqSize);
        }

        private void DrawFigure(Point p, System.Drawing.Image img)
        {
            p.X = p.X * sqSize + offset;
            p.Y = p.Y * sqSize;
            graph.DrawImage(img, p);
        }

        private void PlayWindow_Move(object sender, EventArgs e)
        {
            if (graph != null) ReDraw();
        }

        private void PlayWindow_Deactivate(object sender, EventArgs e)
        {
            if (mouseTracker != null) mouseTracker.Stop();
            if(graph != null) ReDraw();
        }

        private void PlayWindow_Activated(object sender, EventArgs e)
        {
            if (mouseTracker != null && !mouseTracker.Enabled) mouseTracker.Start();
            if (graph != null) ReDraw();
        }


    }
}
