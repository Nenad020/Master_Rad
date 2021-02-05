namespace EasyModbus
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Net.Sockets;

	internal class TCPHandler
	{
		public delegate void DataChanged(object networkConnectionParameter);

		public event DataChanged dataChanged;

		public delegate void NumberOfClientsChanged();

		public event NumberOfClientsChanged numberOfClientsChanged;

		private TcpListener server = null;

		private List<Client> tcpClientLastRequestList = new List<Client>();

		public int NumberOfConnectedClients { get; set; }

		public string ipAddress = null;

		public TCPHandler(int port)
		{
			IPAddress localAddr = IPAddress.Any;
			this.server = new TcpListener(localAddr, port);
			this.server.Start();
			this.server.BeginAcceptTcpClient(this.AcceptTcpClientCallback, null);
		}

		public TCPHandler(string ipAddress, int port)
		{
			this.ipAddress = ipAddress;
			IPAddress localAddr = IPAddress.Any;
			this.server = new TcpListener(localAddr, port);
			this.server.Start();
			this.server.BeginAcceptTcpClient(this.AcceptTcpClientCallback, null);
		}

		private void AcceptTcpClientCallback(IAsyncResult asyncResult)
		{
			TcpClient tcpClient = new TcpClient();
			try
			{
				tcpClient = this.server.EndAcceptTcpClient(asyncResult);
				tcpClient.ReceiveTimeout = 4000;
				if (this.ipAddress != null)
				{
					string ipEndpoint = tcpClient.Client.RemoteEndPoint.ToString();
					ipEndpoint = ipEndpoint.Split(':')[0];
					if (ipEndpoint != this.ipAddress)
					{
						tcpClient.Client.Disconnect(false);
						return;
					}
				}
			}
			catch (Exception)
			{
			}

			try
			{
				this.server.BeginAcceptTcpClient(this.AcceptTcpClientCallback, null);
				Client client = new Client(tcpClient);
				NetworkStream networkStream = client.NetworkStream;
				networkStream.ReadTimeout = 4000;
				networkStream.BeginRead(client.Buffer, 0, client.Buffer.Length, this.ReadCallback, client);
			}
			catch (Exception)
			{
			}
		}

		private int GetAndCleanNumberOfConnectedClients(Client client)
		{
			lock (this)
			{
				bool objetExists = false;
				foreach (Client clientLoop in this.tcpClientLastRequestList)
				{
					if (client.Equals(clientLoop))
						objetExists = true;
				}

				try
				{
					this.tcpClientLastRequestList.RemoveAll(
						delegate (Client c) { return ((DateTime.Now.Ticks - c.Ticks) > 40000000); });
				}
				catch (Exception)
				{
				}

				if (!objetExists)
					this.tcpClientLastRequestList.Add(client);

				return this.tcpClientLastRequestList.Count;
			}
		}

		private void ReadCallback(IAsyncResult asyncResult)
		{
			NetworkConnectionParameter networkConnectionParameter = new NetworkConnectionParameter();
			Client client = asyncResult.AsyncState as Client;
			client.Ticks = DateTime.Now.Ticks;
			this.NumberOfConnectedClients = this.GetAndCleanNumberOfConnectedClients(client);
			if (this.numberOfClientsChanged != null)
				this.numberOfClientsChanged();
			if (client != null)
			{
				int read;
				NetworkStream networkStream = null;
				try
				{
					networkStream = client.NetworkStream;

					read = networkStream.EndRead(asyncResult);
				}
				catch (Exception)
				{
					return;
				}

				if (read == 0)
				{
					// OnClientDisconnected(client.TcpClient);
					// connectedClients.Remove(client);
					return;
				}

				byte[] data = new byte[read];
				Buffer.BlockCopy(client.Buffer, 0, data, 0, read);
				networkConnectionParameter.bytes = data;
				networkConnectionParameter.stream = networkStream;
				if (this.dataChanged != null)
					this.dataChanged(networkConnectionParameter);
				try
				{
					networkStream.BeginRead(client.Buffer, 0, client.Buffer.Length, this.ReadCallback, client);
				}
				catch (Exception)
				{
				}
			}
		}

		public void Disconnect()
		{
			try
			{
				foreach (Client clientLoop in this.tcpClientLastRequestList)
				{
					clientLoop.NetworkStream.Close(00);
				}
			}
			catch (Exception)
			{
			}

			this.server.Stop();
		}

		internal class Client
		{
			private readonly TcpClient tcpClient;

			private readonly byte[] buffer;

			public long Ticks { get; set; }

			public Client(TcpClient tcpClient)
			{
				this.tcpClient = tcpClient;
				int bufferSize = tcpClient.ReceiveBufferSize;
				this.buffer = new byte[bufferSize];
			}

			public TcpClient TcpClient
			{
				get
				{
					return this.tcpClient;
				}
			}

			public byte[] Buffer
			{
				get
				{
					return this.buffer;
				}
			}

			public NetworkStream NetworkStream
			{
				get
				{
					return this.tcpClient.GetStream();
				}
			}
		}
	}
}