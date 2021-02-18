using Common.Communication.Model.SCADA;
using Common.Communication.Access.SCADA;
using Common.Communication.Model.SCADA.Interface;
using EasyModbus;
using Scada.Extensions;
using ScadaDbAccess.Model;
using ScadaService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Scada
{
	public class Poller
	{
		#region Fields

		private static object doPollLocker;

		private readonly ScadaModel scadaModel;

		private readonly int delayInMilliseconds;

		private readonly BitArray currentState;

		private readonly BitArray receivedState;

		private readonly IScadaChanges<bool> scadaCoilAddressChanges;

		private readonly IScadaDbAccess<CoilsAddress> coilAddressesAccess;

		private readonly IScadaDbAccess<HoldingRegistersAddress> holdingRegistersAddressAccess;

		private readonly string modbusIpAddress;

		private readonly int modbusPort;

		private readonly PollingData pollingData;

		private bool doPoll;

		private ModbusClient modbusClient;

		private bool startup;

		#endregion Fields

		public Poller(ScadaModel scadaModel, int delayInMilliseconds, IScadaDbAccess<CoilsAddress> coilAddressesAccess, 
			IScadaDbAccess<HoldingRegistersAddress> holdingRegistersAddressAccess)
		{
			this.startup = true;
			this.pollingData = new PollingData();
			doPollLocker = new object();
			this.DoPoll = false;
			this.delayInMilliseconds = delayInMilliseconds;
			this.scadaModel = scadaModel;
			this.coilAddressesAccess = coilAddressesAccess;
			this.holdingRegistersAddressAccess = holdingRegistersAddressAccess;
			this.currentState = new BitArray(ushort.MaxValue);
			this.receivedState = new BitArray(ushort.MaxValue);
			this.modbusIpAddress = ConfigurationManager.AppSettings["mdbSimIp"];
			this.modbusPort = int.Parse(ConfigurationManager.AppSettings["mdbSimPort"]);
			this.scadaCoilAddressChanges = new ScadaCoilAddressChanges();
			UpdateAddressesToPoll();
			this.startup = false;
		}

		#region Public properties

		private bool DoPoll
		{
			get
			{
				lock (doPollLocker)
				{
					return doPoll;
				}
			}

			set
			{
				lock (doPollLocker)
				{
					doPoll = value;
				}
			}
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

		#endregion Public properties

		#region Methods

		public void UpdateAddressesToPoll()
		{
			var stopped = StopPolling();

			var usedCoilsAddresses = this.scadaModel.UsedCoilsAddress;
			if (usedCoilsAddresses == null || usedCoilsAddresses.Count == 0)
			{
				pollingData.Update(new int[0], new ushort[0], 0);
				return;
			}

			ushort startingAddress = this.scadaModel.MinUsedCoilAddress();
			var endAddress = this.scadaModel.MaxUsedCoilAddress();
			var mdbPollCount = (ushort)(endAddress - startingAddress + 1);

			var addressesToPoll = new ushort[usedCoilsAddresses.Count];
			var idsToPoll = new int[usedCoilsAddresses.Count];

			int i = 0;
			foreach (var usedAddress in usedCoilsAddresses.Values)
			{
				addressesToPoll[i] = (ushort)usedAddress.Address;
				idsToPoll[i] = usedAddress.Id;
				++i;
			}

			pollingData.Update(idsToPoll, addressesToPoll, mdbPollCount);

			//WriteCoilAddressValuesOnSimulator(usedCoilsAddresses);
			//WriteHoldingRegisterAddressValuesOnSimulator(scadaModel.UsedHoldingRegistersAddress);
			//UpdateCurrentState(usedCoilsAddresses.Values.Select(x => x.Value).ToArray());

			Console.WriteLine("Polling addresses updated.");
			if (stopped)
			{
				StartPolling();
			}
		}

		public bool StartPolling()
		{
			if (pollingData.Count == 0)
			{
				DoPoll = false;
				return false;
			}

			if (DoPoll)
			{
				return false;
			}

			DoPoll = true;
			ThreadPool.QueueUserWorkItem(state => PollContinuously());
			//PollContinuously();

			return true;
		}

		public bool StopPolling()
		{
			if (this.startup)
			{
				return false;
			}

			if (!this.DoPoll)
			{
				return true;
			}

			this.DoPoll = false;
			return true;
		}

		private void PollContinuously()
		{
			while (DoPoll)
			{
				lock (doPollLocker)
				{
					if (pollingData.Count == 0)
					{
						break;
					}

					bool[] response;
					var first = pollingData.First();
					ushort startingAddress = (ushort)(first.Value - 1);

					try
					{
						response = ModbusClient.ReadCoils(startingAddress, pollingData.PollCount);
					}
					catch
					{
						Thread.Sleep(delayInMilliseconds);
						continue;
					}

					//UpdateHoldingRegistersAddress(response);
					PollReplyReceived(response);
					Thread.Sleep(delayInMilliseconds);
				}
			}
		}

		private void UpdateHoldingRegistersAddress(bool[] polledValues)
		{
			/*if (polledValues == null || polledValues.Length == 0)
			{
				return;
			}

			var holdingRegisterAddress = ModbusClient.ReadHoldingRegisters(0, 1);

			for (int i = 0; i < polledValues.Length; i++)
			{
				if (polledValues[i])
				{
					holdingRegisterAddress[0]++;
				}
			}

			ModbusClient.WriteSingleRegister(0, holdingRegisterAddress[0]);
			UpdateScadaDbHoldingRegistersAddressValues(0, holdingRegisterAddress[0]);*/
		}

		private void PollReplyReceived(bool[] polledValues)
		{
			if (!DoPoll)
			{
				return;
			}

			polledValues = CleanupPolledValues(polledValues);
			BitArray previousState = UpdateCurrentState(polledValues);
			Change difference = GetDifferenceFor(previousState);

			scadaCoilAddressChanges.Update(difference.Ids, difference.Values);

			//TODO: OBAVESTITI OE O PROMENAMA
			//UpdateScadaDbCoilsAddressValues(difference);

			Console.WriteLine(scadaCoilAddressChanges.Any() ? $"Polled changes:\n{scadaCoilAddressChanges}" : "No changes...");
		}

		private bool[] CleanupPolledValues(bool[] polledValues)
		{
			if (pollingData.Count == polledValues.Length)
			{
				return polledValues;
			}

			if (pollingData.Count == 0)
			{
				throw new ArgumentException("No polled addresses");
			}

			if (polledValues.Length == 0)
			{
				throw new ArgumentException("No polled values");
			}

			bool[] cleanedUpValues = new bool[pollingData.Count];
			var startingAddress = pollingData.First().Value;

			int i = 0;
			foreach (var idAddressPair in pollingData.IdAddressDictionary)
			{
				var addressInArray = idAddressPair.Value - startingAddress;
				cleanedUpValues[i++] = polledValues[addressInArray];
			}

			string assertMessage = "Cleaned up values count not equal to polled addresses count.";
			Debug.Assert(pollingData.Count == cleanedUpValues.Length, assertMessage);

			return cleanedUpValues;
		}

		private BitArray UpdateCurrentState(bool[] values)
		{
			int i = 0;
			foreach (var idAddressPair in pollingData.IdAddressDictionary)
			{
				var address = idAddressPair.Value;
				receivedState[address] = values[i++];
			}

			var previousState = (BitArray)currentState.Clone();
			currentState.VeryDifferentOr(receivedState, pollingData.Addresses);
			return previousState;
		}

		private Change GetDifferenceFor(BitArray previousState)
		{
			List<bool> dynamicValues = new List<bool>();
			List<int> dynamicIds = new List<int>();
			List<ushort> dynamicDiffAddresses = new List<ushort>();

			var diffIndexes = (BitArray)currentState.Clone();
			diffIndexes.Xor(previousState);

			foreach (var gidAddressPair in pollingData.IdAddressDictionary)
			{
				var gid = gidAddressPair.Key;
				var address = gidAddressPair.Value;
				var valueDifferent = diffIndexes[address];
				var currValue = currentState[address];

				if (!valueDifferent)
				{
					continue;
				}

				dynamicValues.Add(currValue);
				dynamicDiffAddresses.Add(address);
				dynamicIds.Add(gid);
			}

			var change = new Change(dynamicDiffAddresses.ToArray(), dynamicIds.ToArray(), dynamicValues.ToArray());
			return change;
		}

		private void WriteCoilAddressValuesOnSimulator()
		{
			//TODO: Ovde pozvati MES i traziti Breaker podatke, pa ispisati one podatke koje mi trebaju
			//		staviti da vraca niz vrednosti bool[]

			/*ushort startingAddress;

			foreach (var coilAddress in coilsAddress)
			{
				startingAddress = (ushort)(coilAddress.Key - 1);

				ModbusClient.WriteSingleCoil(startingAddress, coilAddress.Value.Value);
			}*/
		}

		private void WriteHoldingRegisterAddressValuesOnSimulator()
		{
			//TODO: Ovde pozvati MES i traziti Electicity Meter podatke i ispisati ono sto mi treba

			/*ushort startingAddress;

			foreach (var holdingRegisterAddress in holdingRegistersAddress)
			{
				startingAddress = (ushort)(holdingRegisterAddress.Key - 1);

				ModbusClient.WriteSingleRegister(startingAddress, holdingRegisterAddress.Value.Value);
			}*/
		}

		private void UpdateScadaDbCoilsAddressValues(Change difference)
		{
			/*int newChanges = difference.Addresses.Length;
			List<CoilsAddress> coilsAddresses;

			if (newChanges == 0)
			{
				return;
			}

			coilsAddresses = new List<CoilsAddress>(difference.Addresses.Length);
			for (int i = 0; i < newChanges; i++)
			{
				coilsAddresses.Add(ModelFactory.CreateCoilsAddress(difference.Addresses[i], difference.Ids[i], true, difference.Values[i]));
			}

			coilAddressesAccess.UpdateValue(coilsAddresses);*/
		}

		private void UpdateScadaDbHoldingRegistersAddressValues(int address, int value)
		{
			/*holdingRegistersAddressAccess.UpdateValue(new List<HoldingRegistersAddress>()
			{
				ModelFactory.CreateHoldingRegistersAddress(address + 1, 1, true, value)
			});*/
		}

		#endregion Methods

		#region Private classes

		private class Change
		{
			public Change(ushort[] addresses, int[] ids, bool[] values)
			{
				if (addresses.Length != values.Length && addresses.Length != ids.Length)
				{
					throw new ArgumentException("Count of addresses must be equal to values count");
				}

				Addresses = addresses;
				Values = values;
				Ids = ids;
			}

			public ushort[] Addresses { get; }

			public int[] Ids { get; }

			public bool[] Values { get; }
		}

		private class PollingData
		{
			public PollingData()
			{
				IdAddressDictionary = new Dictionary<int, ushort>();
			}

			public Dictionary<int, ushort> IdAddressDictionary { get; private set; }

			public int PollCount { get; private set; }

			public IEnumerable<ushort> Addresses
			{
				get
				{
					return IdAddressDictionary.Values;
				}
			}

			public int Count
			{
				get
				{
					return IdAddressDictionary.Count;
				}
			}

			public void Update(IEnumerable<int> gids, IEnumerable<ushort> addresses, int pollCount)
			{
				IdAddressDictionary?.Clear();
				var gidsList = gids.ToList();
				var addressesList = addresses.ToList();
				IdAddressDictionary = new Dictionary<int, ushort>(gidsList.Count);

				for (var i = 0; i < gidsList.Count; i++)
				{
					var gid = gidsList[i];
					var address = addressesList[i];
					IdAddressDictionary[gid] = address;
				}

				PollCount = pollCount;
			}

			public KeyValuePair<int, ushort> First()
			{
				return IdAddressDictionary.First();
			}
		}

		#endregion Private classes
	}
}