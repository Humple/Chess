using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Chess.Figures;
using System.Collections.Generic;

namespace Chess.Network
{
	public class BaseNetwork
	{
		//out stack 
		Queue<String> outCommands;
		//network thread
		protected Thread thread;
		//network callback interface
		protected INetworkSupport iNetwork;
		//io socket
		protected Socket socket;
		//buffer size
		protected readonly Int32 BUFF_SIZE = 1024;
		//thread
		protected bool connected;

		public bool IsConnected {
			get {
				return connected;
			}
		}

		public BaseNetwork (INetworkSupport _iNetwork)
		{
			connected = false;
			outCommands = new Queue<String>();
			iNetwork = _iNetwork;
		}

		protected virtual void SocketIO ()
		{
		}

		public bool ReceiveData ()
		{
			DataProcessing( ReceiveCommand() );
			return true;
		}

		protected String ReceiveCommand ()
		{
			byte[] buffer = new byte[BUFF_SIZE];

			int bytes =0;

			while(bytes == 0)
				bytes = socket.Receive(buffer);

			string readed = System.Text.Encoding.UTF8.GetString(buffer);
			string r = readed.Split(NetworkDef.SPLITTER)[0];
			
			#if DEBUG
			Console.WriteLine(this.ToString() +": ReceiveCommand(): readed '" +r +"'");
			#endif
			return r;
		}

		protected void SendCommand (string command)
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(command +NetworkDef.SPLITTER);
			socket.Send (buffer);
		}

		protected void SendCommand()
		{
			SendCommand( GetCommand() );
		}

		public void Disconnect ()
		{
			if (!IsConnected) {
				return;
			} else {
				Console.WriteLine( this.ToString() + ": Socket disconecting...");
				connected = false;
				thread.Join ();
				socket.Shutdown (SocketShutdown.Both);
				socket.Close ();
				iNetwork.Disconnected();
			}
		}

		protected void WaitCommand ()
		{
			while (outCommands.Count < 0) {
				Thread.Sleep(100);
				System.Console.Write (this.ToString() +": Wait for command");
			}
		}

		protected void DataProcessing(string received)
		{
				if(received.StartsWith(NetworkDef.MOVE))
				{
					Position oldPos = new Position(0, 0);
					Position newPos = new Position(0, 0);

					iNetwork.ChessMoved(oldPos, newPos);
				}
				else if(received.StartsWith(NetworkDef.MSG))
				{
					iNetwork.MessageReceived("TODO:");
				}
				else if(received.StartsWith(NetworkDef.END))
				{
					iNetwork.MessageReceived(NetworkDef.END);
				}
		}

		public void Add_MoveFigure (Position oldPos, Position newPos)
		{
			string command = NetworkDef.MOVE +' ' +oldPos.X + ' ' +oldPos.Y
				+' ' +newPos.X +' ' +newPos.Y;

			lock (outCommands) {
				outCommands.Enqueue(command);
			}
		}

		public void Add_Message (string mes)
		{
			string command = NetworkDef.MSG +' ' +mes;

			lock (outCommands) {
				outCommands.Enqueue(command);
			}
		}
	
		public void Add_End (string mes)
		{
			string command = NetworkDef.END;

			lock (outCommands) {
				outCommands.Enqueue(command);
			}
		}

		protected string GetCommand()
		{
			lock (outCommands) {
				string r = outCommands.Dequeue();
				return r;
			}
		}
	}
}

