using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chess
{
	public class NetworkServer
	{
		private INetworkSupport iNetwork;
		//io socket
		private Socket socket;
		//buffer size
		private readonly Int32 BUFF_SIZE = 1024;
		//thread
		Thread serverThread;
		//tcp port listener
		Socket tcpListener;
		//io socket

		public NetworkServer (INetworkSupport _iNetwork)
		{
			iNetwork = _iNetwork;
		}

		public void StartServer()
		{
			tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			tcpListener.Bind ( new IPEndPoint(IPAddress.Any, NetworkDef.PORT) );
			tcpListener.Listen(1);

			serverThread = new Thread(ServerIO);
			serverThread.Name = "ServerThread";
			serverThread.IsBackground = true;
			serverThread.Start();
		}
		
		private void ServerIO()
		{
			socket = tcpListener.Accept();
			tcpListener.Close();
			iNetwork.Connected();


		}


	}
}

