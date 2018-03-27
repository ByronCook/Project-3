using System;
using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
            {
                return;
            }

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Survived":
                    var survivedVariableList = new List<string>
                    {
                        "Survived", "Dead"
                    };
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("Titanic Passengers");
                    comboBox2.Items.Add("Lusitania Passengers");
                    comboBox2.Items.Add("All Passengers");
                    CreateChart(survivedVariableList, "Total amount of passengers", _sorter.GetSurvivedData(TitanicPassengers, LusitaniaPassengers, comboBox1.SelectedItem.ToString()), 2, SeriesChartType.Column);
                    break;
                case "Passengers":
                    var passengersVariableList = new List<string>
                    {
                        "TitanicPassengers",
                        "LusitaniaPassengers"
                    };

                    comboBox2.Items.Clear();

                    comboBox2.Items.Add("Male");
                    comboBox2.Items.Add("Female");
                    comboBox2.Items.Add("All Genders");
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
                        comboBox2.Items.Clear();
                        comboBox2.Items.Add("Class 1");
                        comboBox2.Items.Add("Class 2");
                        comboBox2.Items.Add("Class 3");
                      
                        label2.Text = "Filter on: Boat Class";

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

            if (string.IsNullOrEmpty(comboBox2.SelectedItem.ToString()))
            {
                return;
            }

            switch (comboBox2.SelectedItem.ToString())
            {
                case "Male":
                case "Female":
                    comboBox3.Items.Clear();
                    var countryList = _sorter.GetUniqueCountries(TitanicPassengers, LusitaniaPassengers);

                    foreach (var t in countryList)
                    {
                        comboBox3.Items.Add(string.IsNullOrEmpty(t) ? "All countries" : t);
                    }

                    label4.Text = "Filter on: countries";
                    CreateChart(passengersVariableList, "Total amount of " + comboBox2.SelectedItem +  " passengers per boat", _sorter.GetAllPassengersByGender(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString()), 2, SeriesChartType.Column);
                    break;
                case "All Genders":
                    comboBox3.Items.Clear();
                    var countries = _sorter.GetUniqueCountries(TitanicPassengers, LusitaniaPassengers);

                    foreach (var t in countries)
                    {
                        comboBox3.Items.Add(string.IsNullOrEmpty(t) ? "All countries" : t);
                    }

                    label4.Text = "Filter on: countries";
                    CreateChart(passengersVariableList, "Total amount of passengers per boat", _sorter.GetTotalPassengers(TitanicPassengers, LusitaniaPassengers), 2, SeriesChartType.Column);
                    break;
                case "Lusitania Passengers":
                case "All Passengers":
                case "Titanic Passengers":

                    var stringList = new List<string>
                    {
                        "Survived",
                        "Dead"
                    };
                    comboBox3.Items.Clear();

                    comboBox3.Items.Add("0-12");
                    comboBox3.Items.Add("12-20");
                    comboBox3.Items.Add("20-45");
                    comboBox3.Items.Add("45-65");
                    comboBox3.Items.Add("65-150");
                    comboBox3.Items.Add("Unknown Age");
                    label4.Text = "Filter on: age";
                    CreateChart(stringList, "All passengers from the titanic", _sorter.GetSurvivedData(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString()), 2, SeriesChartType.Column);
                    break;
                
                case "Class 1":
                case "Class 2":
                case "Class 3":
                    {
                        var variableList = new List<string>
                    {
                        "TitanicPassengers",
                        "LusitaniaPassengers",
                    };
                        comboBox3.Items.Clear();
                        comboBox3.Items.Add("Has Family Members");
                        comboBox3.Items.Add("Has No Family Members");
                        comboBox3.Items.Add("All Passengers");

                        CreateChart(variableList, "Total amount of passengers per class", _sorter.GetPassengersByBoatClass(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString()), 2, SeriesChartType.Column);
                    }
                    break;
            }
                
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var countryList = _sorter.GetUniqueCountries(TitanicPassengers, LusitaniaPassengers);
            var country = countryList.Contains(comboBox3.SelectedItem.ToString());
          
            if (string.IsNullOrEmpty(comboBox3.SelectedItem.ToString()))
            {
                return;
            }

            if (country)
            {
                var variableList = new List<string>
                    {
                        "TitanicPassengers",
                        "LusitaniaPassengers"
                    };

                var data = _sorter.GetPassengerByCountryData(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString());
                CreateChart(variableList, "Survived per country, per gender", data, 2, SeriesChartType.Column);
            }

            switch(comboBox3.SelectedItem.ToString())
            {
                case "All Passengers":
                case "Has Family Members":
                case "Has No Family Members":
                        var variableList = new List<string>
                    {
                        "FamilyTitanic",
                        "FamilyLusitania",
                    };

                        CreateChart(variableList, "Family Members", _sorter.GetFamilyMembers(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString()), 2, SeriesChartType.Column);
                    break;

                case "Unknown Age":
                case "0-12":
                case "12-20":
                case "20-45":
                case "45-65":
                case "65-150":
                    var stringList = new List<string>
                    {
                        "Survived",
                        "Dead"
                    };

                    comboBox4.Items.Clear();
                    comboBox4.Items.Add("Percentages");
                    comboBox4.Items.Add("Amount");


                    var data = _sorter.GetSurvivedByAgeCategory(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString());
                    CreateChart(stringList, "Survied per country, per gender", data, 2, SeriesChartType.Column);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox1.FindStringExact("Passengers");
            comboBox2.SelectedIndex = comboBox2.FindStringExact("All Genders");
            comboBox3.SelectedIndex = comboBox3.FindStringExact("All countries");

            label10.Text = "";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox4.SelectedItem.ToString()))
            {
                return;
            }

            switch (comboBox4.SelectedItem.ToString())
            {
                case "Percentages":
                    var stringList = new List<string>
                    {
                        "Survived",
                        "Dead"
                    };

                    var data = _sorter.GetSurvivalRate(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), comboBox4.SelectedItem.ToString());
                    CreateChart(stringList, "Survival rate", data, 2, SeriesChartType.Column);

                    break;
                case "Amount":
                    var varList = new List<string>
                    {
                        "Survived",
                        "Dead"
                    };

                    comboBox4.Items.Clear();
                    comboBox4.Items.Add("Percentages");
                    comboBox4.Items.Add("Amount");


                    var dataList = _sorter.GetSurvivedByAgeCategory(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString());
                    CreateChart(varList, "Survied per country, per gender", dataList, 2, SeriesChartType.Column);
                    break;
                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox1.FindStringExact("Survived");
            comboBox2.SelectedIndex = comboBox2.FindStringExact("Titanic Passengers");
            comboBox3.SelectedIndex = comboBox3.FindStringExact("20-45");

            label10.Text = "";

        }
    }
}