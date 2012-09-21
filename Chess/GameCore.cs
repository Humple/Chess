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
				figuresMatrix = new Figure[8,8];
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
