namespace FuseBoxUI.Helpers
{
	public static class BreakerHelper
	{
		public static double[] GetTogglePositon(bool status)
		{
			if (status)
			{
				return new double[] { 0, 0, 0, 35 }; //On
			}
			else
			{
				return new double[] { 0, 35, 0, 0 }; // Off
			}
		}

		public static string GetBrushColor(bool status)
		{
			if (status)
			{
				return "00c541"; //Green color
			}
			else
			{
				return "ff4747"; //Red color
			}
		}
	}
}
