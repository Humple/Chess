using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chess
{
	public class BaseNetwork
	{
		//network thread
		protected Thread thread;
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
			iNetwork = _iNetwork;
		}

		protected virtual void SocketIO ()
		{
		}

		protected String ReceiveCommand ()
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

		protected void SendCommand (string command)
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
				thread.Join ();
				socket.Shutdown (SocketShutdown.Both);
				socket.Close ();
			}
		}
	}
}

