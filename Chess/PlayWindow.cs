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
        //Field 
        public GuiMatrix matrix = null;
        public readonly Color ColorSelected = Color.PaleGoldenrod;

        private System.Windows.Forms.Timer mouseTracker;

        private int sqSize { get; set; }   // размер квадрата
        private int offset { get; set; }   // отступ от края формы
        private BufferedGraphics graph = null;
        private System.Drawing.Pen pen = null;
        private Chess.IGameControl control;

        private delegate void DrawAsyncDelegate(object sender, EventArgs e);

        public PlayWindow(Chess.IGameControl gameControl, string title, Chess.GuiMatrix guiMatrix)
        {
            this.Text = title;
            InitializeComponent();
            matrix = guiMatrix;
            control = gameControl;
            sqSize = 80;
            offset = 25;
        }

        //draw chess field
        private void Draw(object sender, EventArgs e)
        {
            if (matrix == null)
                return;
            if (e != null)
                Thread.Sleep((int)sender);
            Spot tmpSpot;
            pen.Color = this.BackColor;
            graph.Graphics.FillRectangle(pen.Brush, 0, 0, sqSize * 8 + offset + 1, sqSize * 8 + offset + 1);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    if ((tmpSpot = matrix.GetSpot(i, j)) == null)
                        continue;

                    //Выбор цвета квадрата

                    if (tmpSpot.Highlighted)	//highlighted spot
                        if (tmpSpot.sColor.R != 255)
                            pen.Color = Color.FromArgb(50, 100, 75);
                        else
                            pen.Color = Color.FromArgb(100, 255, 125);
                    else if (tmpSpot.Selected) 	//selected spot
                        pen.Color = ColorSelected;
                    else 						//simple spot
                        pen.Color = tmpSpot.sColor;

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

                    //отрисовка изображения фигуры
                    if (tmpSpot.image != null)
                    {
                        DrawFigure(new Position(i, j), tmpSpot.image);
                    }
                }
            }

            //literal drawing
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

            graph.Render();
        }
        //draw image on field in p position
        private void DrawFigure(Position p, System.Drawing.Image img)
        {
            graph.Graphics.DrawImage(img, p.X * sqSize + offset, p.Y * sqSize);
        }
        // Sync redrawing
        private void ReDraw()
        {
            Draw(null, null);
        }
        // Manual synced/asynced redrawing
        private void ReDraw(bool async)
        {
            if (async)
            {
                DrawAsyncDelegate d = new DrawAsyncDelegate(Draw);
                d.BeginInvoke(null, null, null, null);
            }
            else
                Draw(null, null);
        }
        //Асинхронная отрисовкай с задержкой на delay милисекунд
        private void ReDraw(int delay)
        {
            DrawAsyncDelegate d = new DrawAsyncDelegate(Draw);
            d.BeginInvoke(delay, new EventArgs(), null, null);
        }
        //Window shown event handler
        private void PlayWindow_Shown(object sender, EventArgs e)
        {
            BufferedGraphicsContext graphContext = BufferedGraphicsManager.Current;
            graphContext.MaximumBuffer = new System.Drawing.Size(sqSize * 8 + offset + 1, sqSize * 8 + offset + 1);
            graph = graphContext.Allocate(this.CreateGraphics(), new Rectangle(0, 0, sqSize * 8 + offset + 1, sqSize * 8 + offset + 1));

            pen = new System.Drawing.Pen(Color.CadetBlue);

            mouseTracker = new System.Windows.Forms.Timer();
            mouseTracker.Interval = 40;
            mouseTracker.Tick += new EventHandler(PlayWindow_MouseTracking);
            mouseTracker.Start();

            ReDraw(50);
        }
        //Cursor moved event
        private void PlayWindow_MouseTracking(object sender, EventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            pt.X -= offset;
            pt.X /= sqSize;
            pt.Y /= sqSize;
            if (pt.X >= 8 || pt.Y >= 8 || pt.X < 0 || pt.Y < 0 || 
                !(control.SpotFocused(new Position(pt.X, pt.Y)) ||
                (matrix.GetSpot(pt.X, pt.Y) != null && matrix.GetSpot(pt.X, pt.Y).Highlighted)))
                return;
            if (matrix.SetFocused(pt.X, pt.Y))
                ReDraw(true);
        }
        //Mouse down event handler
        private void PlayWindow_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);

            pt.X -= offset;
            pt.X /= sqSize;
            pt.Y /= sqSize;

            if (pt.X >= 8 || pt.Y >= 8 || pt.X < 0 || pt.Y < 0)
                return;
            Position mouseClickedPos = new Position(pt.X, pt.Y);


            if (control.SpotFocused(mouseClickedPos) && matrix.SetSelected(pt.X, pt.Y))
            {
                //invoke interface method
                control.SpotSelected(mouseClickedPos);
            }
            else if (matrix.GetSpot(mouseClickedPos) != null && matrix.GetSpot(mouseClickedPos).Highlighted)
            {
                //Метод передвижения фигуры
                MessageBox.Show("Фигура передвинута", "DEBUG");
            }
            ReDraw(true);
        }
        //If window moved. Window events handlers
        private void PlayWindow_Move(object sender, EventArgs e)
        {
            if (graph != null)
                ReDraw(true);
        }
        //If window deactivated
        private void PlayWindow_Deactivate(object sender, EventArgs e)
        {
            if (mouseTracker != null)
                mouseTracker.Stop();
            if (graph != null)
                ReDraw(true);
        }
        //If windows activated
        private void PlayWindow_Activated(object sender, EventArgs e)
        {
            if (mouseTracker != null && !mouseTracker.Enabled)
                mouseTracker.Start();
            if (graph != null)
                ReDraw(true);
        }
    }
}
