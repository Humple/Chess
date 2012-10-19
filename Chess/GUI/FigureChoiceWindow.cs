using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chess.Figures;

namespace Chess.GUI
{
    public partial class FigureChoiceWindow : Form
    {
        private const int sqSize = 80;
        private BufferedGraphics graph = null;
        private System.Drawing.Pen pen = null;
        private int FocusedSpot = 0;
        private System.Windows.Forms.Timer mouseTracker;
        //private bool formBusy = false;
        private Figure[] arr;

        private int result;
        public Figure Result { get { return arr[result]; } }

        public FigureChoiceWindow(Point startPosition, FigureColor clr)
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(2 * sqSize, 2 * sqSize);
            this.Location = startPosition;
            arr = new Figure[4];
            arr[0] = new Queen(clr);
            arr[1] = new Rock(clr);
            arr[2] = new Knight(clr);
            arr[3] = new Bishop(clr);
        }

        private void FigureChoiceWindow_Click(object sender, EventArgs e)
        {
            mouseTracker.Stop();
            Point pt = PointToClient(Cursor.Position);
            pt.X /= sqSize;
            pt.Y /= sqSize;

            if (pt.X < 1)
            {
                if (pt.Y < 1) result = 0;
                else result = 2;
            }
            else
            {
                if (pt.Y < 1) result = 1;
                else result = 3;
            }
            this.Close();
        }

        private void FigureChoiceWindow_Load(object sender, EventArgs e)
        {
            BufferedGraphicsContext graphContext = BufferedGraphicsManager.Current;
            graphContext.MaximumBuffer = new System.Drawing.Size((Point)this.Size);
            graph = graphContext.Allocate(this.CreateGraphics(), new Rectangle(new Point(0, 0), this.Size));
            pen = new System.Drawing.Pen(Color.CadetBlue);

            mouseTracker = new System.Windows.Forms.Timer();
            mouseTracker.Interval = 40;
            mouseTracker.Tick -= new EventHandler(MouseTracking);
            mouseTracker.Tick += new EventHandler(MouseTracking);
            mouseTracker.Start();

            ReDraw(50);
        }
        private void Draw(object sender, EventArgs e)
        {
            //while (formBusy) System.Threading.Thread.Sleep(1);
            //formBusy = true;
            pen.Color = Color.LightYellow;
            graph.Graphics.FillRectangle(pen.Brush, 0, 0, 2 * sqSize, 2 * sqSize);
            int i, j;
            switch (FocusedSpot)
            {
                case 0: i = 0; j = 0; break;
                case 1: i = 0; j = 1; break;
                case 2: i = 1; j = 0; break;
                case 3: i = 1; j = 1; break;
                default: i = 0; j = 0; break;
            }
            pen.Color = Color.Gold;
            graph.Graphics.FillRectangle(pen.Brush, sqSize * i, sqSize * j, sqSize, sqSize);
            pen.Color = Color.Gray;
            graph.Graphics.DrawRectangle(pen, 0, 0, 2 * sqSize - 1, 2 * sqSize - 1);
            graph.Graphics.DrawImage(arr[0].image, 0, 0);
            graph.Graphics.DrawImage(arr[1].image, sqSize, 0);
            graph.Graphics.DrawImage(arr[2].image, 0, sqSize);
            graph.Graphics.DrawImage(arr[3].image, sqSize, sqSize);
            graph.Render();
            //formBusy = false;
        }
        private void DrawFigure(Position p, System.Drawing.Image img)
        {
            graph.Graphics.DrawImage(img, p.X * sqSize, p.Y * sqSize);
        }
        // Sync drawing
        private void ReDraw()
        {
            Draw(null, null);
        }
        // Manual sync/async drawing
        private void ReDraw(bool async)
        {
            if (async)
            {
                PlayWindow.DrawAsyncDelegate d = new PlayWindow.DrawAsyncDelegate(Draw);
                d.BeginInvoke(null, null, null, null);
            }
            else
                Draw(null, null);
        }
        //Асинхронная отрисовкай с задержкой на delay милисекунд
        private void ReDraw(int delay)
        {
            PlayWindow.DrawAsyncDelegate d = new PlayWindow.DrawAsyncDelegate(Draw);
            d.BeginInvoke(delay, new EventArgs(), null, null);
        }

        private void MouseTracking(object sender, EventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            pt.X /= sqSize;
            pt.Y /= sqSize;

            if (pt.X < 1)
            {
                if (pt.Y < 1) FocusedSpot = 0;
                else FocusedSpot = 1;
            }
            else 
            {
                if (pt.Y < 1) FocusedSpot = 2;
                else FocusedSpot = 3;
            }

            ReDraw(true);
        }
    }
}
