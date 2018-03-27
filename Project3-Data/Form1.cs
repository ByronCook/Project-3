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
            if(comboBox3.SelectedItem == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(comboBox3.SelectedItem.ToString()))
            {
                return;
            }
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

                        comboBox4.Items.Clear();
                        comboBox4.Items.Add("Survival Rate");
                        comboBox4.Items.Add("Amount of Passengers");
                        label4.Text = "Filter on: ship";

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
                    CreateChart(stringList, "Survived per boat, per age group", data, 2, SeriesChartType.Column);

                    
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            comboBox1.SelectedIndex = comboBox1.FindStringExact("Passengers");
            comboBox2.SelectedIndex = comboBox2.FindStringExact("All Genders");
            comboBox3.SelectedIndex = comboBox3.FindStringExact("All countries");
            label14.Text = "It is quite clear that both the Titanic and the Lusitania both had a lot more male \npassengers than female passengers. With a total of almost 2000 passengers \non the Lusitania, just above 1400 of those were male. The Titanic had \nabout 1400 passengers and a little bit over 800 of those were male. \nWhen comparing the amount of passengers per country, it is evident that most \npassengers came from either the United States, Canada or the United Kingdom. \nBut there are also differences in other countries, for example sweden. \nThe titanic had 40 swedish passengers whereas the Lusitania only had 1";
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
                case "Survival Rate":
                case "Amount of Passengers":
                    var stringList2 = new List<string>
                    {
                        "SurvivedTitanicPassengers",
                        "SurvivedLusitaniaPassengers"
                    };

                    CreateChart(stringList2, "Survival rate in percentages", _sorter.GetSurvivalRatePerBoatClass(TitanicPassengers, LusitaniaPassengers, comboBox2.SelectedItem.ToString(), comboBox3.SelectedItem.ToString(), comboBox4.SelectedItem.ToString()), 2, SeriesChartType.Column);

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
                    CreateChart(varList, "Survied per boat, per age group", dataList, 2, SeriesChartType.Column);
                    break;
                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox1.SelectedIndex = comboBox1.FindStringExact("Boat Class");
            comboBox2.SelectedIndex = comboBox2.FindStringExact("Class 1");
            comboBox3.SelectedIndex = comboBox3.FindStringExact("Has Family Members");
            comboBox4.SelectedIndex = comboBox4.FindStringExact("Survival Rate");

            label14.Text = "Both cruisers show that there a noteable differences in \nsurvival rate depending on class and family members on board, \nwith RMS Titanic seeing the biggest spike of survival in first class with \npassengers who had family aboard. Other classes don't show as \nmuch of a difference of survival rate whether they had family on \nboard or not. This could be explained by the Titanic being  \na 'A-class' ocean liner and thus being popular with the upper class. \nOverall survival rates are generally higher on RMS Titanic, \nsomething that could be attributed to the lenghty two and half hour \nsinking as opposed to the fairly quick 18 minute sinking \nof RMS Lusitania which would give passengers of the former \nmore time to act. Seeing that RMS Lusitania sunk due to an \nattack as opposed to the supposed accident that sunk RMS \nTitanic could help understand the significant differences \nof survivability between the two liners.";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox1.SelectedIndex = comboBox1.FindStringExact("Survived");
            comboBox2.SelectedIndex = comboBox2.FindStringExact("Titanic Passengers");
            comboBox3.SelectedIndex = comboBox3.FindStringExact("20-45");
            label14.Text = "No, there is no significant difference in survival rate \nper age group. Both ships show a trend slightly \nfavoring younger individuals, peaking at the age group \nof 12 – 20 years old. Our hypothesis is that it is \nlinked to general health and fitness of the individuals, \nmainly because these people can rush to safety much faster \nwith less effort than the elderly and also more efficient and \nwith caution than kids below 12.";
        }
    }
}