namespace ScadaDbAccess.Model
{
	public static class ModelFactory
	{
		public static CoilsAddress CreateCoilsAddress(int address, int id, bool used)
		{
			var coilAddress = new CoilsAddress
			{
				Address = address,
				Id = id,
				Used = used
			};

			return coilAddress;
		}

		public static HoldingRegistersAddress CreateHoldingRegistersAddress(int address, int id, bool used)
		{
			var holdingRegistersAddress = new HoldingRegistersAddress
			{
				Address = address,
				Id = id,
				Used = used
			};

			return holdingRegistersAddress;
		}
	}
}