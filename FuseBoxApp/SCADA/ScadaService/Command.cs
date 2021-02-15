using Common.Communication.Contract.SCADA;
using Common.Exceptions.SCADA;
using EasyModbus;
using ScadaDbAccess.Model;
using System.Configuration;
using System.ServiceModel;

namespace ScadaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Command : ICommand
    {
        private readonly string modbusIpAddress;

        private readonly int modbusPort;

        private readonly ScadaModel scadaModel;

        private ModbusClient modbusClient;

        internal Command()
        {
        }

        public Command(ScadaModel scadaModel)
        {
            this.scadaModel = scadaModel;
            modbusIpAddress = ConfigurationManager.AppSettings["mdbSimIp"];
            modbusPort = int.Parse(ConfigurationManager.AppSettings["mdbSimPort"]);
        }

        private ModbusClient ModbusClient
        {
            get
            {
                if (modbusClient == null)
                {
                    modbusClient = new ModbusClient(modbusIpAddress, modbusPort);
                }

                if (!modbusClient.Connected)
                {
                    modbusClient.Connect();
                }

                return modbusClient;
            }
        }

        public bool Open(int id)
        {
            try
            {
                CoilsAddress coilAddress;
                if (!scadaModel.TryGetUsedCoilAddress(id, out coilAddress))
                {
                    throw new CoilAddressNotExistsException();
                }

                return WriteSingleCoil((ushort)coilAddress.Address, false);
            }
            catch (CoilAddressNotExistsException)
            {
                throw new FaultException<IdNotExistsFault>(new IdNotExistsFault(id), $"Address with ID {id} doesn't exist.");
            }
        }

        public bool Close(int id)
        {
            try
            {
                CoilsAddress coilAddress;
                if (!scadaModel.TryGetUsedCoilAddress(id, out coilAddress))
                {
                    throw new CoilAddressNotExistsException();
                }

                return WriteSingleCoil((ushort)coilAddress.Address, true);
            }
            catch (CoilAddressNotExistsException)
            {
                throw new FaultException<IdNotExistsFault>(new IdNotExistsFault(id), $"Address with ID {id} doesn't exist.");
            }
        }

        private bool WriteSingleCoil(ushort address, bool value)
        {
            try
            {
                ModbusClient.WriteSingleCoil(address - 1, value);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}