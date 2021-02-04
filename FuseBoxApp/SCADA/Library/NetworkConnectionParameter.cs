namespace EasyModbus
{
	using System.Net;
	using System.Net.Sockets;

	struct NetworkConnectionParameter
	{
		public NetworkStream stream; // For TCP-Connection only

		public byte[] bytes;
	}
}