namespace Scada.Extensions
{
	using System.Collections;
	using System.Collections.Generic;

	public static class BitArrayExtensions
	{
		////  |0|1|0|1|
		////  |0|0|1|1|
		//// =|0|1|0|1|
		public static BitArray VeryDifferentOr(this BitArray arrayToModify, BitArray arrayToCompare, IEnumerable<ushort> indexesToCheck)
		{
			arrayToModify.Or(arrayToCompare);
			foreach (int idx in indexesToCheck)
			{
				var bit1 = arrayToCompare[idx];
				var bit2 = arrayToModify[idx];
				bool valueAtIndex;

				if (bit1 & bit2)
				{
					// [1 1] => 1
					valueAtIndex = true;
				}
				else if (!bit1 & !bit2)
				{
					// [0 0] => 0
					valueAtIndex = false;
				}
				else if (bit1 & !bit2)
				{
					// [1 0] => 1
					valueAtIndex = true;
				}
				else
				{
					// [0 1] => 0
					valueAtIndex = false;
				}

				arrayToModify[idx] = valueAtIndex;
			}

			return arrayToModify;
		}
	}
}