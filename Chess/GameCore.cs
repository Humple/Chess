using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chess;
using Chess.Figures;

namespace Chess
{

    class GameCore: IGameControl
    {

		private PlayWindow playWindow;
		private Figure[,] figuresMatrix;

		public GameCore ()
		{

		}

        public void InitGame()
        {
            playWindow = new PlayWindow("Chess", this);
            playWindow.FormClosed += new FormClosedEventHandler(Close);
            Application.EnableVisualStyles();
            playWindow.Show();
            Application.Run();
        }

        private void Close(Object o, FormClosedEventArgs e)
        {
            Application.Exit();
        }

		private void StartGame ()
		{
			//WHITE
			figuresMatrix = new Figure[8, 8];
				
			figuresMatrix [0, 7] = new Rock (FigureColor.WHITE);
			figuresMatrix [7, 7] = new Rock (FigureColor.WHITE);

			figuresMatrix [6, 7] = new Knight (FigureColor.WHITE);
			figuresMatrix [1, 7] = new Knight (FigureColor.WHITE);

			figuresMatrix [2, 7] = new Bishop (FigureColor.WHITE);
			figuresMatrix [5, 7] = new Bishop (FigureColor.WHITE);

			figuresMatrix [3, 7] = new King (FigureColor.WHITE);
			figuresMatrix [4, 7] = new Queen (FigureColor.WHITE);

			for (int i = 0; i<8; i++) {
				figuresMatrix[i, 6] = new Pawn(FigureColor.WHITE);
			}

			//BLACK
			figuresMatrix [0, 0] = new Rock (FigureColor.BLACK);
			figuresMatrix [7, 0] = new Rock (FigureColor.BLACK);

			figuresMatrix [6, 0] = new Knight (FigureColor.BLACK);
			figuresMatrix [1, 0] = new Knight (FigureColor.BLACK);

			figuresMatrix [2, 0] = new Bishop (FigureColor.BLACK);
			figuresMatrix [5, 0] = new Bishop (FigureColor.BLACK);

			figuresMatrix [3, 0] = new King (FigureColor.BLACK);
			figuresMatrix [4, 0] = new Queen (FigureColor.BLACK);

			for (int i = 0; i<8; i++) {
				figuresMatrix[i, 1] = new Pawn(FigureColor.BLACK);
			}


		}

		//IGameControl
		public void FigureMoved(Position oldPos, Position newPos)
		{

		}

		public void SpotSelected(Position spotPos)
		{

		}

		public void StartButtonClicked()
		{

		}

		public void StopButtonClicked()
		{

		}
    }
}
