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
		private bool connected;
		//connected state
		public bool IsConnected {
			get {
				return connected;
			}
		}
		public NetworkServer (INetworkSupport _iNetwork)
		{
			iNetwork = _iNetwork;
			connected = false;
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
		
		private void ServerIO ()
		{
			socket = tcpListener.Accept ();
			tcpListener.Close ();
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

		private String ReceiveCommand ()
		{
			byte[] buffer = new byte[BUFF_SIZE];

			int bytes =0;

			while(bytes == 0)
				bytes = socket.Receive(buffer);

#if DEBUG
			Console.WriteLine("ReceiveCommand(): readed " + bytes );
#endif
			string readed = System.Text.Encoding.UTF8.GetString(buffer);
			string r = readed.Split('\n')[0];
			return r;
		}

		private void SendCommand (string command)
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(command +"\n");
			socket.Send (buffer);
		}

		public void Disconnect ()
		{
			if (IsConnected) {
				return;
			} else {
				connected = false;
				serverThread.Join ();
				socket.Shutdown (SocketShutdown.Both);
				socket.Close ();
				iNetwork.Disconnected();
			}
		}


	}
}

