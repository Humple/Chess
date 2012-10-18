using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chess;
using Chess.Figures;

namespace Chess
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

        public GameCore()
        {
            Application.EnableVisualStyles();
            runColor = FigureColor.WHITE;
            pState = new PlayersState();
        }

        public void Initialize()
        {
            matrix = new CoreMatrix();
            playWindow = new PlayWindow(this, "Chess", new GuiMatrix(matrix));
            playWindow.FormClosed += new FormClosedEventHandler(PlayWindowClose);
            inviteWindow = new InviteWindow();
            inviteWindow.OnChoice += new InviteWindow.OnChoiceEventHandler(InviteWindowMessageReceived);
            inviteWindow.Show();
            Application.Run();
        }

		public void Initialize(string ip)
        {

			matrix = new CoreMatrix();
            playWindow = new PlayWindow(this, "Chess", new GuiMatrix(matrix));
            playWindow.FormClosed += new FormClosedEventHandler(PlayWindowClose);


	  		if(ip =="0.0.0.0")
				StartServer();
			else
				StartClient(ip);

			Application.Run ();
        }

        private void ReInitialize()
        {
            runColor = FigureColor.WHITE;
            matrix = new CoreMatrix();
            playWindow = new PlayWindow(this, "Chess", new GuiMatrix(matrix));
            playWindow.FormClosed += new FormClosedEventHandler(PlayWindowClose);

            inviteWindow = new InviteWindow();
            inviteWindow.OnChoice += new InviteWindow.OnChoiceEventHandler(InviteWindowMessageReceived);
            inviteWindow.Show();
        }

        private void CheckForMate()
        {
            pState.ResetGameState();

            for (int i = 0; i < 8; i++)
            { 		//y
                for (int j = 0; j < 8; j++)
                { 	//x
                    Position currentPos = new Position(j, i);

                    if (matrix.HasFigureAt(currentPos))
                    {
                        Figure currentFigure = matrix.FigureAt(currentPos);
                        FigureColor currentColor = currentFigure.Color;
                        FigureColor enemyColor = (currentColor == FigureColor.WHITE) ? (FigureColor.BLACK) : (FigureColor.WHITE);

                        Position kingPos = matrix.GetKing(enemyColor);
                        Console.WriteLine("King position is " + kingPos.X + ' ' + kingPos.Y);
                        List<Position> atack = currentFigure.GetAvailableAtackPositons(currentPos, matrix);

                        if (atack.Contains(kingPos))
                        {
                            string message;

                            if (runColor == enemyColor)
                            {
                                playWindow.matrix.SetChecked(currentPos);
                                playWindow.matrix.SetChecked(kingPos);
                                message = "Check!";
                                pState.SetState(enemyColor, PlayerState.CHECK);
                            }
                            else
                            {
                                pState.SetState(enemyColor, PlayerState.CHECKMATE);
                                message = "CheckMate!";
                            }

                            MessageBox.Show(message, "Information");
                        }
                    }
                }
            }

            if (pState.GetState(FigureColor.BLACK) == PlayerState.CHECKMATE ||
                pState.GetState(FigureColor.WHITE) == PlayerState.CHECKMATE)
                EndGame();
        }

        private void StartLocal()
        {
            	playWindow.NetworkEnabled = false;
				strokeLock = false;
				endGameLock = false;
				playWindow.Show();
        }

        private void EndGame ()
		{
			endGameLock = true;

			if (network != null) {
				network.Disconnect ();
				network = null;
			}
        }

        private void MoveFigure(Position oldPos, Position newPos)
        {
            matrix.MoveFigure(oldPos, newPos);
            playWindow.matrix.MoveImage(oldPos, newPos);
            playWindow.matrix.ResetAllAttribures();

            runColor = (runColor == FigureColor.WHITE) ? (FigureColor.BLACK) : (FigureColor.WHITE);
            CheckForMate();
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

        private void StartClient(string ip)
        {
			playWindow.NetworkEnabled = true;
			strokeLock = false;
			endGameLock = false;

            System.Console.WriteLine("Connecting to server: " + ip);
            try
            {
                strokeLock = true;
               network = new NetworkClient(this);
                ((NetworkClient) network).ConnetcToServer(ip);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                Console.WriteLine(e.Message);
            }
        }

        #region gui delegates
        private void PlayWindowClose(Object o, FormClosedEventArgs e)
        {
            ReInitialize();
        }

        private void InviteWindowMessageReceived(Object o, OnChoiceEventArgs e)
        {
            switch (e.Type)
            {
                case OnChoiceEventArgs.ConnectionType.OFFLINE:
					StartLocal();
                    break;
                case OnChoiceEventArgs.ConnectionType.SERVER:
                    StartServer();
                    break;
                case OnChoiceEventArgs.ConnectionType.CLIENT:
                    StartClient(e.IP);
                    break;
                case OnChoiceEventArgs.ConnectionType.EXIT:
                    Application.Exit();
                    break;
            }
        }
        #endregion

        #region INetworkSupport implementation
        public void ChessMoved(Position oldPos, Position newPos)
        {
			strokeLock = !strokeLock;
            MoveFigure(oldPos, newPos);
        }

        public void Connected()
        {
            System.Console.WriteLine(this.ToString() + ": Connected()");
			if(inviteWindow != null ) 
				inviteWindow.Close ();

			playWindow.Show();
        }

        public void Disconnected()
        {
			System.Console.WriteLine(this.ToString() + ": Disconnected()");
        }

        public void MessageReceived(string mes)
        {
            playWindow.PrintToConsole("Network message: " + mes, System.Drawing.Color.Green);
        }
        #endregion

        #region IGameControl implementation
        public void FigureMoved(Position oldPos, Position newPos)
        {

        }

        public void SpotSelected(Position spotPos)
        {
            if (endGameLock || strokeLock)
                return;

            Console.WriteLine("Square selected: " + spotPos.X + ' ' + spotPos.Y);


            //select a figure and highlig
            if (matrix.HasFigureAt(spotPos))
            {
                Figure figure = matrix.FigureAt(spotPos);

                if (figure.Color == runColor)
                {
                    figure.ConsolePrintPosition(spotPos);

                    if (playWindow.matrix.SetSelected(spotPos))
                        playWindow.matrix.SetHighlighted(figure.GetAvailableAtackPositons(spotPos, matrix));
                    figurePos = spotPos;
                }
            }

            //moving figure to new position
            if (playWindow.matrix.GetSpot(spotPos).Highlighted && figurePos != null)
            {
                Console.WriteLine("Moving figure from: " + figurePos.X + ' ' + figurePos.Y + " to " + spotPos.X + ' ' + spotPos.Y);

                if (runColor == FigureColor.WHITE)
                {
                    playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                    playWindow.PrintToConsoleLn("Player1 moved " + matrix.FigureAt(figurePos).ToString() + " from " +
                        (char)('A' + figurePos.X) + Convert.ToString(8 - figurePos.Y) + " to " +
                        (char)('A' + spotPos.X) + Convert.ToString(8 - spotPos.Y), System.Drawing.Color.FromArgb(64, 128, 255));
                }
                else
                {
                    playWindow.PrintToConsole("System: ", System.Drawing.Color.Red);
                    playWindow.PrintToConsoleLn("Player2 moved " + matrix.FigureAt(figurePos).ToString() + " from " +
                        (char)('A' + figurePos.X) + Convert.ToString(8 - figurePos.Y) + " to " +
                        (char)('A' + spotPos.X) + Convert.ToString(8 - spotPos.Y), System.Drawing.Color.FromArgb(128, 64, 255));
                }

                MoveFigure(figurePos, spotPos);
                FigureMoved(figurePos, spotPos);
            }
        }

        public bool SpotFocused(Position spotPos)
        {
            if (endGameLock || strokeLock)
                return false;

            if ((matrix.HasFigureAt(spotPos) && matrix.FigureAt(spotPos).Color == runColor)
                || playWindow.matrix.GetSpot(spotPos).Highlighted)
                return true;
            else
                return false;
        }

        #endregion
    }
}
