namespace ScadaService
{
	using Common.Interfaces.SCADA.Access;
	using ScadaDbAccess.Model;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ScadaModel
	{
		private IScadaDbAccess<CoilsAddress> coilAddressesAccess;

		private IScadaDbAccess<HoldingRegistersAddress> holdingRegistersAddressAccess;

		public Dictionary<int, CoilsAddress> UsedCoilsAddress { get; private set; }

		public Dictionary<int, HoldingRegistersAddress> UsedHoldingRegistersAddress { get; private set; }

		public event EventHandler UsedAddressesUpdated;

		public ScadaModel(IScadaDbAccess<CoilsAddress> coilAddressesAccess, IScadaDbAccess<HoldingRegistersAddress> holdingRegistersAddressAccess)
		{
			this.coilAddressesAccess = coilAddressesAccess;
			this.holdingRegistersAddressAccess = holdingRegistersAddressAccess;

			this.UsedCoilsAddress = new Dictionary<int, CoilsAddress>();
			this.UsedHoldingRegistersAddress = new Dictionary<int, HoldingRegistersAddress>();
		}

		public void Initialize()
		{
			var usedCoilsAddress = this.coilAddressesAccess.GetAllUsedEntities();
			var usedHoldingRegistersAddress = this.holdingRegistersAddressAccess.GetAllUsedEntities();

			this.UsedCoilsAddress = new Dictionary<int, CoilsAddress>(usedCoilsAddress.Count);
			foreach (var usedCoilAddress in usedCoilsAddress)
			{
				this.UsedCoilsAddress.Add(usedCoilAddress.Id, usedCoilAddress);
			}

			this.UsedHoldingRegistersAddress = new Dictionary<int, HoldingRegistersAddress>(usedHoldingRegistersAddress.Count);
			foreach (var usedHoldingAddress in usedHoldingRegistersAddress)
			{
				this.UsedHoldingRegistersAddress.Add(usedHoldingAddress.Id, usedHoldingAddress);
			}

			this.OnUsedAddressesUpdated();
		}

		public ushort MinUsedCoilAddress()
		{
			return (ushort)this.UsedCoilsAddress.Min(pair => pair.Value.Address);
		}

		public ushort MinUsedHoldingAddress()
		{
			return (ushort)this.UsedHoldingRegistersAddress.Min(pair => pair.Value.Address);
		}

		public ushort MaxUsedCoilAddress()
		{
			return (ushort)this.UsedCoilsAddress.Max(pair => pair.Value.Address);
		}

		public ushort MaxUsedHoldingAddress()
		{
			return (ushort)this.UsedHoldingRegistersAddress.Max(pair => pair.Value.Address);
		}

		protected virtual void OnUsedAddressesUpdated()
		{
			this.UsedAddressesUpdated?.Invoke(this, EventArgs.Empty);
		}

		public bool TryGetUsedCoilAddress(int id, out CoilsAddress usedCoilAddress)
		{
			return this.UsedCoilsAddress.TryGetValue(id, out usedCoilAddress);
		}
	}
}