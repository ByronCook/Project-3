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
        private List<Passenger> LusitaniaPassengers { get; set; }

        public Form1()
        {
            InitializeComponent();
            //_sorter.PopulateDatabase();
        }

        private void LoadData()
        {
            var dataReader = new DataReader();
            TitanicPassengers = dataReader.CreateTitanicPassengerList();
            LusitaniaPassengers = dataReader.CreateLusitaniaPassengerList();
        }

        private void Form1_Load(object sender, EventArgs e) // Executed when the program loads for the first time
        {
            LoadData();
           // _sorter.GetSurvivalRate(TitanicPassengers);
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
//                case "Survived":
//                    var survivedVariableList = new List<string>
//                    {
//                        "Survived", "Dead"
//                    };
//
//                    comboBox2.Items.Add("USA");
//
//                    CreateChart(survivedVariableList, "Titanic", _sorter.GetSurvivedData(TitanicPassengers), 2, SeriesChartType.Column);
//                    break;
//                case "Age":
//                    var ageVariableList = new List<string>
//                    {
//                        "AgeUnder30", "AgeOver30", "AgeUnknown"
//                    };
//                    
//                    CreateChart(ageVariableList, "Titanic", _sorter.GetAgeData(TitanicPassengers), 3, SeriesChartType.Column);
//                        break;
//                case "Surival Rate":
//                    var survivalRateVariableList = new List<string>
//                    {
//                        "MaleSurivalRate",
//                        "FemalesAndKidsSurvivalRate"
//                    };
//                    CreateChart(survivalRateVariableList, "Surival Rate Titanic in Percentages", _sorter.GetSurvivalRate(TitanicPassengers),2, SeriesChartType.Column);
//                    break;
                case "Passengers":
                    var passengersVariableList = new List<string>
                    {
                        "TitanicPassengers",
                        "LusitaniaPassengers"
                    };

                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("Male");
                    comboBox2.Items.Add("Female");
                    comboBox2.Items.Add("All");
                    label2.Text = "Filter on: Gender";

                    CreateChart(passengersVariableList, "Total amount of passengers per boat", _sorter.GetTotalPassengers(TitanicPassengers, LusitaniaPassengers), 2, SeriesChartType.Column);
                    break;
                case "Boat Class":
                    {
                        var variableList = new List<string>
                    {
                        "FirstClassTitanic",
                        "SecondClassTitanic",
                        "ThirdClassTitanic",

                        "FirstClassLusitania",
                        "SecondClassLusitania",
                        "ThirdClassLusitania"
                    };
                        CreateChart(variableList, "Boat Class", _sorter.GetBoatClass(TitanicPassengers, LusitaniaPassengers), 6, SeriesChartType.Column);
                    }
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var passengersVariableList = new List<string>
                    {
                        "TitanicPassengers",
                        "LusitaniaPassengers"
                    };


            var countryList = _sorter.GetUniqueCountries(TitanicPassengers, LusitaniaPassengers);

            foreach (var t in countryList)
            {
                comboBox3.Items.Add(string.IsNullOrEmpty(t) ? "All countries" : t);
            }

            switch (comboBox2.SelectedItem.ToString())
            {
                case "Male":
                case "Female":
                    CreateChart(passengersVariableList, "Total amount of passengers per boat", _sorter.GetAllPassengersByGender(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString()), 2, SeriesChartType.Column);
                    break;
                case "All":
                    CreateChart(passengersVariableList, "Total amount of passengers per boat", _sorter.GetTotalPassengers(TitanicPassengers, LusitaniaPassengers), 2, SeriesChartType.Column);
                    break;
            }
                
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var countryList = _sorter.GetUniqueCountries(TitanicPassengers, LusitaniaPassengers);
            var country = countryList.Contains(comboBox3.SelectedItem.ToString());
            // Waarde bestaat in de lijst -> pak passangers per gender & waarde

            if (country)
            {
                var variableList = new List<string>
                    {
                        "TitanicPassengers",
                        "LusitaniaPassengers"
                    };

                var data = _sorter.GetPassengerByCountryData(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString());
                CreateChart(variableList, "Survied per country, per gender", data, 2, SeriesChartType.Column);
            }
        }
    }
}