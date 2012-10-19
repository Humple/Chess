using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chess.GUI;
using Chess.Figures;
using Chess.Network;

namespace Chess.Core
{
	class GameCore : IGameControl, INetworkSupport
	{
		private PlayWindow playWindow;
		private InviteWindow inviteWindow;
		private CoreMatrix matrix;
		private Chess.Figures.FigureColor runColor;
		private Position figurePos;
		private PlayersState pState;
		private bool endGameLock, strokeLock;
		private BaseNetwork network;

		public GameCore ()
		{
			Application.EnableVisualStyles ();
			runColor = FigureColor.WHITE;
			pState = new PlayersState ();
		}

		public void Initialize ()
		{
			matrix = new CoreMatrix ();
			playWindow = new PlayWindow (this, "Chess", new GuiMatrix (matrix));
			playWindow.FormClosed += new FormClosedEventHandler (PlayWindowClose);
			inviteWindow = new InviteWindow ();
			inviteWindow.OnChoice += new InviteWindow.OnChoiceEventHandler (InviteWindowMessageReceived);
			inviteWindow.Show ();
			Application.Run ();
		}

		public void Initialize (string ip)
		{

			matrix = new CoreMatrix ();
			playWindow = new PlayWindow (this, "Chess", new GuiMatrix (matrix));
			playWindow.FormClosed += new FormClosedEventHandler (PlayWindowClose);


			if (ip == "0.0.0.0")
				StartServer ();
			else
				StartClient (ip);

			Application.Run ();
		}

		private void ReInitialize ()
		{
			runColor = FigureColor.WHITE;
			matrix = new CoreMatrix ();
			playWindow = new PlayWindow (this, "Chess", new GuiMatrix (matrix));
			playWindow.FormClosed += new FormClosedEventHandler (PlayWindowClose);

			inviteWindow = new InviteWindow ();
			inviteWindow.OnChoice += new InviteWindow.OnChoiceEventHandler (InviteWindowMessageReceived);
			inviteWindow.Show ();
		}

		private void CheckForMate (FigureColor kingColor)
		{
			if (King.IsShahState (matrix, kingColor)) {
				int solutionsCount = 0;

				for (int i=0; i<8; i++)
					for (int j=0; j<8; j++) {
						Position curPos = new Position (i, j);
						if (matrix.HasFigureAt (curPos)) {
							Figure figure = matrix.FigureAt (curPos);

							if (figure.Color == kingColor) {
								solutionsCount += figure.GetAvailableOnShahPositons (curPos, matrix).Count;
							}
						}
					}

				if (solutionsCount > 0) {
					pState.SetState (kingColor, PlayerState.CHECK);
					Console.WriteLine ("King in danger! ");


				} else {
					pState.SetState (kingColor, PlayerState.CHECKMATE);	
					Console.WriteLine ("King have some trouble! ");
				}

				playWindow.matrix.SetChecked (matrix.GetKing (kingColor));
			}
		}

		private void CheckForMate ()
		{
			System.Console.WriteLine ("Checking state...");
			pState.ResetGameState ();

			CheckForMate (FigureColor.BLACK);
			CheckForMate (FigureColor.WHITE);

			PlayerState black = pState.GetState (FigureColor.BLACK);
			PlayerState white = pState.GetState (FigureColor.WHITE);


			if (black == PlayerState.CHECK) {
				playWindow.PrintToConsole ("System: ", System.Drawing.Color.Red);
				playWindow.PrintToConsoleLn("Player 2: check!", System.Drawing.Color.Black);
			} else if (black == PlayerState.CHECKMATE) {
				playWindow.PrintToConsole ("System: ", System.Drawing.Color.Red);
				playWindow.PrintToConsoleLn("Player 2: mate!", System.Drawing.Color.Black);
			}

			if (white == PlayerState.CHECK) {
				playWindow.PrintToConsole ("System: ", System.Drawing.Color.Red);
				playWindow.PrintToConsoleLn("Player 2: check!", System.Drawing.Color.Black);
			} else if (white == PlayerState.CHECKMATE) {
				playWindow.PrintToConsole ("System: ", System.Drawing.Color.Red);
				playWindow.PrintToConsoleLn("Player 1: mate!", System.Drawing.Color.Black);
			}

			if (pState.GetState (FigureColor.BLACK) == PlayerState.CHECKMATE ||
				pState.GetState (FigureColor.WHITE) == PlayerState.CHECKMATE)
				EndGame ();
		}

		private void StartLocal ()
		{
			playWindow.NetworkEnabled = false;
			strokeLock = false;
			endGameLock = false;
			playWindow.Show ();
		}

		private void EndGame ()
		{
			endGameLock = true;

			if (network != null) {
				network.Disconnect ();
				network = null;
			}
		}

		private void MoveFigure (Position oldPos, Position newPos)
		{

			if (runColor == FigureColor.WHITE) {
				playWindow.PrintToConsole ("System: ", System.Drawing.Color.Red);
				playWindow.PrintToConsoleLn ("Player1 moved " + matrix.FigureAt (oldPos).ToString () + " from " +
					(char)('A' + oldPos.X) + Convert.ToString (8 - oldPos.Y) + " to " +
					(char)('A' + newPos.X) + Convert.ToString (8 - newPos.Y), System.Drawing.Color.FromArgb (64, 128, 255));
			} else {
				playWindow.PrintToConsole ("System: ", System.Drawing.Color.Red);
				playWindow.PrintToConsoleLn ("Player2 moved " + matrix.FigureAt (oldPos).ToString () + " from " +
					(char)('A' + oldPos.X) + Convert.ToString (8 - oldPos.Y) + " to " +
					(char)('A' + newPos.X) + Convert.ToString (8 - newPos.Y), System.Drawing.Color.FromArgb (128, 64, 255));
			}

			matrix.MoveFigure (oldPos, newPos);
			playWindow.matrix.MoveImage (oldPos, newPos);
			playWindow.matrix.ResetAllAttribures ();

			runColor = (runColor == FigureColor.WHITE) ? (FigureColor.BLACK) : (FigureColor.WHITE);
			CheckForMate ();
		}

		private void StartServer ()
		{
			playWindow.NetworkEnabled = true;
			strokeLock = false;
			endGameLock = false;
			System.Console.WriteLine (this.ToString () + " InitServer() ");
			try {
				network = new NetworkServer (this);
				((NetworkServer)network).StartServer ();
			} catch (Exception e) {
				MessageBox.Show (e.Message, "Server Error");
			}

		}

		private void StartClient (string ip)
		{
			playWindow.NetworkEnabled = true;
			strokeLock = false;
			endGameLock = false;

			System.Console.WriteLine ("Connecting to server: " + ip);
			try {
				strokeLock = true;
				network = new NetworkClient (this);
				((NetworkClient)network).ConnetcToServer (ip);
			} catch (Exception e) {
				MessageBox.Show (e.Message, "Error");
				Console.WriteLine (e.Message);
			}
		}

        #region gui delegates
		private void PlayWindowClose (Object o, FormClosedEventArgs e)
		{
			ReInitialize ();
		}

		private void InviteWindowMessageReceived (Object o, OnChoiceEventArgs e)
		{
			switch (e.Type) {
			case OnChoiceEventArgs.ConnectionType.OFFLINE:
				StartLocal ();
				break;
			case OnChoiceEventArgs.ConnectionType.SERVER:
				StartServer ();
				break;
			case OnChoiceEventArgs.ConnectionType.CLIENT:
				StartClient (e.IP);
				break;
			case OnChoiceEventArgs.ConnectionType.EXIT:
				Application.Exit ();
				break;
			}
		}
        #endregion

        #region INetworkSupport implementation
		public void ChessMoved (Position oldPos, Position newPos)
		{
			strokeLock = !strokeLock;
			MoveFigure (oldPos, newPos);
		}

		public void Connected ()
		{
			System.Console.WriteLine (this.ToString () + ": Connected()");
			if (inviteWindow != null) 
				inviteWindow.Close ();

			playWindow.Show ();
		}

		public void Disconnected ()
		{
			System.Console.WriteLine (this.ToString () + ": Disconnected()");
		}

		public void MessageReceived (string mes)
		{
			playWindow.PrintToConsole ("Network message: " + mes, System.Drawing.Color.Green);
		}
        #endregion

        #region IGameControl implementation
		public void FigureMoved (Position oldPos, Position newPos)
		{

		}

		public void SpotSelected (Position spotPos) 
		{
			if (endGameLock || strokeLock)
				return;

			Console.WriteLine ("Square selected: " + spotPos.X + ' ' + spotPos.Y);
			//moving figure
			if (playWindow.matrix.IsHighlighted (spotPos)) {
				MoveFigure (figurePos, spotPos);
				FigureMoved (figurePos, spotPos);
				return;
			}

			//select a figure and highligt
			if (matrix.HasFigureAt (spotPos)) {
				Figure figure = matrix.FigureAt (spotPos);

				if (figure.Color == runColor && playWindow.matrix.SetSelected (spotPos)) {	
					playWindow.matrix.SetHighlighted (figure.GetAvailableOnShahPositons (spotPos, matrix));
					figurePos = spotPos;
				}
			}

		}

		public bool SpotFocused (Position spotPos)
		{
			if (endGameLock || strokeLock)
				return false;

			if ((matrix.HasFigureAt (spotPos) && matrix.FigureAt (spotPos).Color == runColor)
				|| playWindow.matrix.GetSpot (spotPos).Highlighted)
				return true;
			else
				return false;
		}

        #endregion
	}
}
