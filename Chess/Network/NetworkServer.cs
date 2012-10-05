using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chess
{
	public class NetworkServer: BaseNetwork
	{
		private Socket tcpListener;

		public NetworkServer (INetworkSupport _iNetwork): base(_iNetwork)
		{

		}

		public void StartServer()
		{
			tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			tcpListener.Bind ( new IPEndPoint(IPAddress.Any, NetworkDef.PORT) );
			tcpListener.Listen(1);

			thread = new Thread(SocketIO);
			thread.Name = "ServerThread";
			thread.IsBackground = true;
			thread.Start();
		}

		protected override void SocketIO ()
		{
			socket = tcpListener.Accept ();
			tcpListener.Close ();


			SendCommand (NetworkDef.VERSION);

			if (ReceiveCommand () == NetworkDef.OK)
				Console.WriteLine (" Version accepted");
			else {
				Disconnect ();
				return;
			}

			iNetwork.Connected ();

			while (IsConnected) {

				String received = ReceiveCommand();
				if( received == NetworkDef.OK )	{
				}
				else{
					Disconnect();
				}
			}
		}
}
}