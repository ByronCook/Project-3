using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project3_Data
{
    public partial class Form1 : Form
    {
        private List<Passenger> TitanicPassengers { get; set; } 
        private List<ChartData> ChartData { get; set; } 
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var dataReader = new DataReader();
            TitanicPassengers = dataReader.CreatePassengerList();
        }

        private void Form1_Load(object sender, System.EventArgs e) // Executed when the program loads for the first time
        {
            LoadData();
        }

        private void CreateChart(string primaryString, string secondaryString, string dataName)
        {
            chart1.DataSource = ChartData;
            chart1.Series.Add(primaryString).YValueMembers = primaryString;
            chart1.Series[primaryString].AxisLabel = dataName;
            chart1.Series[primaryString].ChartType = SeriesChartType.Column;
            chart1.Series[primaryString].XValueType = ChartValueType.Int32;
            chart1.Series[primaryString].YValueType = ChartValueType.Int32;

            chart1.Series.Add(secondaryString).YValueMembers = secondaryString;
            chart1.Series[secondaryString].ChartType = SeriesChartType.Column;
            chart1.Series[secondaryString].XValueType = ChartValueType.Int32;
            chart1.Series[secondaryString].YValueType = ChartValueType.Int32;

            chart1.DataBind();
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Survived")
            {
                GetSurvivedData();
                CreateChart("Survived", "Dead", "Titanic");
            }
            //else if (comboBox1.SelectedItem.ToString() == "Age")
        }

        private void GetSurvivedData()
        {
            var survivedTitanicPassengers = TitanicPassengers.Count(t => t.Survived);
            var deadTitanicPassengers = TitanicPassengers.Count(t => !t.Survived);

            ChartData = new List<ChartData>
            {
                new ChartData {Survived = survivedTitanicPassengers, Dead = deadTitanicPassengers, Female = 10}
            };
            //  data.Add(new ChartData()); // 2nd boat & dataset
        }
    }
}