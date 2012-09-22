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
        private InviteWindow inviteWindow;
        private CoreMatrix matrix;
		private Chess.Figures.FigureColor runColor;

		public GameCore ()
		{
            Application.EnableVisualStyles();
            
		}

        public void Initialize()
        {
            matrix = new CoreMatrix();
            playWindow = new PlayWindow(this, "Chess", new GuiMatrix(matrix));
            playWindow.FormClosed += new FormClosedEventHandler(PlayWindowClose);
            inviteWindow = new InviteWindow();
            inviteWindow.FormClosed += new FormClosedEventHandler(InviteWindowClose);
            inviteWindow.Show();
            Application.Run();
        }

        private void ReInitialize()
        {
            matrix = new CoreMatrix();
            playWindow.FieldMatrix = new GuiMatrix(matrix);
            playWindow = new PlayWindow(this, "Chess", new GuiMatrix(matrix));
            playWindow.FormClosed += new FormClosedEventHandler(PlayWindowClose);
            inviteWindow = new InviteWindow();
            inviteWindow.FormClosed += new FormClosedEventHandler(InviteWindowClose);
            inviteWindow.Show();
        }

        private void PlayWindowClose(Object o, FormClosedEventArgs e)
        {
            ReInitialize();
        }
        private void InviteWindowClose(Object o, FormClosedEventArgs e)
        {
            switch (inviteWindow.returnValue)
            {
                case "Offline":
                {
                    playWindow.Show();
                } break;
                case "Online":
                {
                    Application.Exit();
                } break;
                default:
                {
                    Application.Exit();
                } break;
            }
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
			if(matrix.HasFigureAt(spotPos)){
				//playWindow.FieldMatrix.SetHighlighted( matrix.FigureAt(spotPos).GetAvailableMovePossitons(spotPos));
			}
		}

		public void StartButtonClicked()
		{
			runColor = FigureColor.WHITE;
		}

		public void StopButtonClicked()
		{

		}
    }
}
