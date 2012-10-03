using System;

namespace Chess
{
	public class NetworkServer
	{
		private INetworkSupport iNetwork;

		public NetworkServer (INetworkSupport _iNetwork)
		{
			iNetwork = _iNetwork;
		}

		public void StartServer(Int16 port)
		{

		}

	}
}

