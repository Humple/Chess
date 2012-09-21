using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Chess.Figures;

namespace Chess
{
    class GameCore: IGameControl
    {

		private PlayWindow playWindow;

		public void InitGame ()
		{
			playWindow = new PlayWindow("Chess");

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(playWindow);
		}


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
