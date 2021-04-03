using Common.Communication.Client.MES;
using Common.Model.Report;
using FuseBoxUI.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Input;

namespace FuseBoxUI.ViewModel.Report
{
	public class ReportViewModel : BaseViewModel
    {
        private string selectedReport = "Alarm history";

        public List<string> Reports { get; set; }

        public string SelectedReport
		{
            get
			{
                return selectedReport;
			}
            set
			{
                selectedReport = value;
                ChangeDatePickerVisibility(value);
			}
		}

        public bool DatePickerVisible { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DataTable Data { get; set; }

        public ICommand GenerateReportCommand { get; set; }

        public ReportViewModel()
        {
            Reports = new List<string>()
            {
                "Current equipment",
                "Alarm history"
            };

            DatePickerVisible = true;

            Data = new DataTable();

            FromDate = new DateTime(2021, 3, 1);
            ToDate = new DateTime(2021, 4, 15);

            GenerateReportCommand = new RelayCommand(GenerateReport);

            GenerateReport();
        }

        public void GenerateReport()
        {
            using (ReportsClient client = new ReportsClient())
            {
                try
                {
                    ReportModel result = null;
                    if (SelectedReport == "Current equipment")
					{
                        result = client.ActiveEquipment();
                    }
                    else
					{
                        result = client.AlarmHistory(FromDate, ToDate);
					}

                    Data = new DataTable();

					foreach (var header in result.Headers)
					{
						Data.Columns.Add(new DataColumn(header));
					}

					foreach (var row in result.Rows.Values)
					{
						Data.Rows.Add(row.ToArray());
					}

					OnPropertyChanged(nameof(Data));
                }
                catch
                {
                    throw;
                }
            }
        }

        private void ChangeDatePickerVisibility(string report)
		{
            if (report == "Current equipment")
			{
                DatePickerVisible = true;
                return;
			}

            DatePickerVisible = false;
		}

        private void UpdateDataGrid(ReportModel result)
		{
            Data = new DataTable();

			for (int i = 0; i < 5; i++)
			{
				Data.Columns.Add(new DataColumn(i.ToString()));
			}

            //foreach (var header in result.Headers)
            //{
            //             Data.Columns.Add(new DataColumn(header));
            //}

            //foreach (var row in result.Rows.Values)
            //{
            //    Data.Rows.Add(row.ToArray());
            //}

            //for (int i = 6; i < 15; i++)
            //{
            //    Data.Rows.Add("new message is very very very very very very longggggggggggggg", i.ToString());
            //}

            //OnPropertyChanged(nameof(Data));
		}
	}
}
