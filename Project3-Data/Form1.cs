using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project3_Data
{
    public partial class Form1 : Form
    {
        private readonly DataSorter _sorter = new DataSorter();
        private List<Passenger> TitanicPassengers { get; set; } 

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var dataReader = new DataReader();
            TitanicPassengers = dataReader.CreatePassengerList();
        }

        private void Form1_Load(object sender, EventArgs e) // Executed when the program loads for the first time
        {
            LoadData();
            _sorter.GetSurvivalRate(TitanicPassengers);
        }

        private void CreateChart(IReadOnlyList<string> variableList, string dataName, List<ChartData> data, int dataCount, SeriesChartType chartType)
        {
            chart1.Series?.Clear();
            chart1.DataSource = data;
            var count = 0;
            while (count < dataCount)
            {
                AddChartSeries(variableList[count], chartType);
                count++;
            }

            if (chart1.Series != null) chart1.Series[variableList[0]].AxisLabel = dataName;
            chart1.DataBind();
        }

        private void AddChartSeries(string dataName, SeriesChartType chartType)
        {
            if (dataName != null)
            {
                chart1.Series.Add(dataName).YValueMembers = dataName;
                chart1.Series[dataName].ChartType = chartType;
                chart1.Series[dataName].XValueType = ChartValueType.Int32;
                chart1.Series[dataName].YValueType = ChartValueType.Int32;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Survived":
                {
                    var variableList = new List<string>
                    {
                        "Survived", "Dead"
                    };

                    CreateChart(variableList, "Titanic", _sorter.GetSurvivedData(TitanicPassengers), 2, SeriesChartType.Column);
                }
                    break;
                case "Age":
                {
                    var variableList = new List<string>
                    {
                        "AgeUnder30", "AgeOver30", "AgeUnknown"
                    };
                    
                    CreateChart(variableList, "Titanic", _sorter.GetAgeData(TitanicPassengers), 3, SeriesChartType.Column);
                }
                    break;
                case "Surival Rate":
                {
                    var variableList = new List<string>
                    {
                        "MaleSurivalRate",
                        "FemalesAndKidsSurvivalRate"
                    };
                    CreateChart(variableList, "Surival Rate Titanic in Percentages", _sorter.GetSurvivalRate(TitanicPassengers),2, SeriesChartType.Column);
                }
                    break;
            }
        }
    }
}