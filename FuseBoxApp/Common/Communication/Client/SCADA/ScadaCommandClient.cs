using Common.Communication.Contract.SCADA;
using System.ServiceModel;

namespace Common.Communication.Client.SCADA
{
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public class ScadaCommandClient : ClientBase<ICommand>, ICommand
	{
		public ScadaCommandClient()
		{
		}

		public ScadaCommandClient(string endpointConfigurationName)
		   : base(endpointConfigurationName)
		{
		}

		public bool Open(int id)
		{
			return Channel.Open(id);
		}

		public bool Close(int id)
		{
			return Channel.Close(id);
		}
	}
}