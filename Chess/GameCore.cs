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
        private CoreMatrix matrix;

		public GameCore ()
		{

		}

        public void InitGame()
        {
            matrix = new CoreMatrix();
            playWindow = new PlayWindow("Chess", this, matrix);
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
