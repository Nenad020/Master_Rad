namespace ScadaDbAccess.Model
{
	public static class ModelFactory
	{
		public static CoilsAddress CreateCoilsAddress(int address, int id, bool used, bool value)
		{
			var coilAddress = new CoilsAddress
			{
				Address = address,
				Id = id,
				Used = used,
				Value = value
			};

			return coilAddress;
		}

		public static HoldingRegistersAddress CreateHoldingRegistersAddress(int address, int id, bool used, int value)
		{
			var holdingRegistersAddress = new HoldingRegistersAddress
			{
				Address = address,
				Id = id,
				Used = used,
				Value = value
			};

			return holdingRegistersAddress;
		}
	}
}
