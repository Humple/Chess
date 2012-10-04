using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Chess
{
	public class NetworkClient
	{
		//client socket
		private Socket socket;
		//network interface
		private INetworkSupport inetsupport;
		//buffer size
		private readonly Int32 BUFF_SIZE = 1024;
		//thread
		Thread clientThread;
		//

		private bool connected;

		public bool IsConnected { 
			get { 
				return connected;	
			}
		}

		public NetworkClient (INetworkSupport isuppport)
		{
			inetsupport = isuppport;
			connected = false;
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
			inetsupport.Connected();

			clientThread = new Thread( NetIO );
			clientThread.IsBackground = true;
			clientThread.Name = "ClientThread";
			clientThread.Start ();
		}

		private void NetIO ()
		{
			while (IsConnected) {

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
			return System.Text.Encoding.UTF8.GetString(buffer);
		}

		private void SendCommand (string command)
		{
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(command);
			socket.Send (buffer);
		}

		public void Disconnect ()
		{
			if (IsConnected) {
				return;
			} else {
				connected = false;
				clientThread.Join ();
				socket.Shutdown (SocketShutdown.Both);
				socket.Close ();
			}
		}

		
	}
}

