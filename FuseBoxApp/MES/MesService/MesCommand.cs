using Common.Communication.Client.SCADA;
using Common.Communication.Contract.MES;
using System.ServiceModel;

namespace MesService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class MesCommand : IMESCommand
	{
		public bool Close(int id)
		{
			using (ScadaCommandClient client = new ScadaCommandClient())
			{
				try
				{
					client.Close(id);
					return true;
				}
				catch
				{
					return false;
				}
			}
		}

		public bool Open(int id)
		{
			using (ScadaCommandClient client = new ScadaCommandClient())
			{
				try
				{
					client.Open(id);
					return true;
				}
				catch
				{
					return false;
				}
			}
		}
	}
}
