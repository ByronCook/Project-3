using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Project3_Data
{
    class DataSorter
    {
//        public List<ChartData> GetSurvivedData(List<Passenger> dataList)
//        {
//            var survivedTitanicPassengers = dataList.Count(t => t.Survived);
//            var deadTitanicPassengers = dataList.Count(t => !t.Survived);
//
//            return new List<ChartData>
//            {
//                new ChartData {Survived = survivedTitanicPassengers, Dead = deadTitanicPassengers, Female = 10}
//                // new ChartData(); //2nd boat
//            };
//        }
//
//        public List<ChartData> GetAgeData(List<Passenger> dataList)
//        {
//
//            var under30 = dataList.Count(e => e.Age < 30 && e.Age > 0);
//            var over30 = dataList.Count(t => t.Age >= 30);
//            var ageUnknown = dataList.Count(y => y.Age == 0);
//
//            return new List<ChartData>
//            {
//                new ChartData {AgeUnder30 = under30, AgeOver30 = over30, AgeUnknown = ageUnknown}
//            };
//        }

//        public List<ChartData> GetSurvivalRate(List<Passenger> dataList)
//        {
//            var males = dataList.Where(e => e.Gender == "Male" || e.Gender == "male");
//            var females = dataList.Where(e => e.Gender == "Female" || e.Gender == "female");
//            var maleKids = males.Where(e => e.Age < 18);
//
//            var survivedMales = males.Where(e => e.Survived);
//            var survivedFemales = females.Where(e => e.Survived);
//
//            var survivedMaleAdults = males.Where(e => e.Age >= 18 && e.Survived);
//            var survivedMalesKids = maleKids.Where(e => e.Survived);
//
//            var adultMaleSurvivalRate = (survivedMaleAdults.Count()*100)/males.Count();
//            var femalesAndKidsSurvivalRate = ((survivedFemales.Count() + survivedMalesKids.Count())*100/
//                                              (females.Count() + maleKids.Count()));
//
//            return new List<ChartData>
//            {
//                new ChartData
//                {
//                    MaleSurivalRate = adultMaleSurvivalRate,
//                    FemalesAndKidsSurvivalRate = femalesAndKidsSurvivalRate
//                }
//            };
//        }

        public List<ChartData> GetPassengerByCountryData(List<Passenger> titanicList, List<Passenger> lusitaniaList, string gender, string country)
        {
            var tPassengers = titanicList.Where(e => e.Gender == gender|| e.Gender == gender);
            var lPassengers = lusitaniaList.Where(e => e.Gender == gender || e.Gender == gender);


            var tFromCountry = tPassengers.Count(t => t.Country == country);
            var lFromCountry = lPassengers.Count(t => t.Country == country);
            Console.WriteLine(lFromCountry + " " + tFromCountry);
            return new List<ChartData>
            {
                new ChartData
                {
                    TitanicPassengers = tFromCountry,
                    LusitaniaPassengers = lFromCountry
                }
            };
        } 

        // vraag Jorren en Joyce
        public List<ChartData> GetBoatClass(List<Passenger> titanicList, List<Passenger> lusitaniaList)
        {
            var firstClassTitanic = titanicList.Count(t => t.BoatClass == "First");
            var secondClassTitanic = titanicList.Count(t => t.BoatClass == "Second");
            var thirdClassTitanic = titanicList.Count(t => t.BoatClass == "Third");

            var firstClassLusitania = lusitaniaList.Count(l => l.BoatClass == "First");
            var secondClassLusitania = lusitaniaList.Count(l => l.BoatClass == "Second");
            var thirdClassLusitania = lusitaniaList.Count(l => l.BoatClass == "Third");

            var survivedTitanic = titanicList.Where(t => t.Survived);
            var survivedLusitania = lusitaniaList.Where(l => l.Survived);

            Console.WriteLine(firstClassLusitania);

            return new List<ChartData>
            {
                new ChartData
                {
                    FirstClassTitanic = firstClassTitanic,
                    SecondClassTitanic = secondClassTitanic,
                    ThirdClassTitanic = thirdClassTitanic,

                    FirstClassLusitania = firstClassLusitania,
                    SecondClassLusitania = secondClassLusitania,
                    ThirdClassLusitania = thirdClassLusitania
                }
            };
        }


         public void PopulateDatabase()
         {
            SqlConnection sqlConn =
                new SqlConnection(
                    @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True");

            SqlDataAdapter da = new SqlDataAdapter();


            
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName;
            var filePath = projectPath + "\\resource\\Titanic_Sorted_v1.csv";

            var passengerLines = File.ReadAllLines(filePath);

            foreach (var item in passengerLines)
            {
                if (passengerLines.ElementAt(0) == item)
                {
                    continue;
                }

                if (passengerLines.ElementAt(1310) == item)
                {
                    return;
                }

                var values = item.Split(',');

                da.InsertCommand =
                    new SqlCommand(
                        "INSERT INTO TitanicPassengers VALUES(@boatclass,@survived,@name,@gender,@age,@country)",
                        sqlConn);
                da.InsertCommand.Parameters.Add("@boatclass", SqlDbType.VarChar).Value = values[0];
                da.InsertCommand.Parameters.Add("@survived", SqlDbType.Int).Value = values[1];
                da.InsertCommand.Parameters.Add("@name", SqlDbType.VarChar).Value = values[2];
                da.InsertCommand.Parameters.Add("@gender", SqlDbType.VarChar).Value = values[3];
                da.InsertCommand.Parameters.Add("@age", SqlDbType.VarChar).Value = values[4];
                da.InsertCommand.Parameters.Add("@country", SqlDbType.VarChar).Value = values[5];
                sqlConn.Open();
                da.InsertCommand.ExecuteNonQuery();

                da.SelectCommand = new SqlCommand("SELECT * FROM TitanicPassengers");
                sqlConn.Close();
            }
        }

        public List<ChartData> GetTotalPassengers(List<Passenger> titanicPassengers, List<Passenger> lusitaniaPassengers)
        {
            var tpassengers = titanicPassengers.Count;
            var lpassengers = lusitaniaPassengers.Count;

            return new List<ChartData>
            {
                new ChartData
                {
                    TitanicPassengers = tpassengers,
                    LusitaniaPassengers = lpassengers
                }
            };
        }

        public List<string> GetUniqueCountries(List<Passenger> titanicPassengers, List<Passenger> lustianiaPassengers)
        {
            var distinctTitanicCountries = titanicPassengers.Select(t => t.Country).Distinct();
            var distinctLusitaniaCountries = lustianiaPassengers.Select(t => t.Country).Distinct();

            return distinctTitanicCountries.Union(distinctLusitaniaCountries).ToList();
        }

        public List<ChartData> GetAllPassengersByGender(List<Passenger> titanicPassengers, List<Passenger> lusitaniaPassengers, string gender)
        {
            var malesTitanic = titanicPassengers.Count(t => t.Gender == gender || t.Gender == gender);
            var malesLusitania = lusitaniaPassengers.Count(t => t.Gender == gender || t.Gender == gender);

            return new List<ChartData>
            {
                new ChartData
                {
                    TitanicPassengers = malesTitanic,
                    LusitaniaPassengers = malesLusitania
                }
            };
         }
    }
}
