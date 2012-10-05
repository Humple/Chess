using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Chess
{
	public class NetworkClient: BaseNetwork
	{

		public NetworkClient (INetworkSupport isuppport): base(isuppport)
		{
		}

		public void ConnetcToServer (string ip)
		{
			if( IsConnected ) {
				throw new SocketException(255);
			}

			IPAddress serverIP = IPAddress.Parse (ip);
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(serverIP, NetworkDef.PORT );


			//invoke intrerface notify
			connected = true;
			iNetwork.Connected();
			thread = new Thread( SocketIO );
			thread.IsBackground = true;
			thread.Name = "ClientThread";
			thread.Start ();
		}

		protected override void SocketIO ()
		{
			if( ReceiveCommand() == NetworkDef.VERSION )
				iNetwork.Connected();
			else
			{
				Disconnect();
				return;
			}
			while (IsConnected) {
				string s = ReceiveCommand();
				Console.WriteLine(" SocketIO(): " +s);
			}
		}
	}
}

