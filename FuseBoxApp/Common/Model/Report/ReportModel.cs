using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Common.Model.Report
{
	[DataContract]
	public class ReportModel
	{
		[DataMember]
		public List<string> Headers { get; set; }

		[DataMember]
		public Dictionary<int, List<string>> Rows { get; set; }

		private int rowCount;

		public ReportModel()
		{
			Headers = new List<string>();
			Rows = new Dictionary<int, List<string>>();

			rowCount = 1;
		}

		public ReportModel(List<string> headers)
		{
			Headers = headers;
			Rows = new Dictionary<int, List<string>>();

			rowCount = 1;
		}

		public void AddRow(params string[] values)
		{
			Rows.Add(rowCount, values.ToList());
			rowCount++;
		}
	}
}
