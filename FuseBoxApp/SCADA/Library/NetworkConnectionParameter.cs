namespace EasyModbus
{
	using System.Net.Sockets;

	internal struct NetworkConnectionParameter
	{
		public NetworkStream stream; // For TCP-Connection only

		public byte[] bytes;
	}
}