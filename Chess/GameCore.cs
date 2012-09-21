using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

		public void InitGame ()
		{
			playWindow = new PlayWindow("Chess");

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			playWindow.Show();
			Application.Run();
		}

		private void StartGame ()
		{
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
