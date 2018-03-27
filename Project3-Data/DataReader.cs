using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project3_Data
{
    class DataReader
    {
        public List<Passenger> CreateTitanicPassengerList()
        {
            var passengerList = new List<Passenger>();

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName;
            var filePath = projectPath + "\\resource\\Titanic_Sorted_v1.csv";
            
            var passengerLines = File.ReadAllLines(filePath);
            
            foreach (var item in passengerLines)
            {
                if (passengerLines.ElementAt(0) == item)
                {
                    continue;
                }
                var values = item.Split(','); // leeftijd, overleeft, classe, geslacht, country, 

                var siblings = values[7] != "" ? Convert.ToInt32(values[7]) : 0;
                var parents = values[8] != "" ? Convert.ToInt32(values[8]) : 0;
                passengerList.Add(new Passenger
                {
                    BoatClass = values[0],
                    Survived = values[1] != "" && Convert.ToBoolean(Convert.ToInt16(values[1])),
                    FirstName = values[3],
                    LastName = values[4],
                    Gender = values[5],
                    Age = values[6] != "" ? Convert.ToDouble(values[6]) : 0,
                    Country = values[14],
                    FamilyMembers = siblings + parents
                });
            }

            return passengerList;
        }

        public List<Passenger> CreateLusitaniaPassengerList()
        {
            var passengerList = new List<Passenger>();

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName;
            var filePath = projectPath + "\\resource\\Lusitania_Sorted_v1.csv";

            var passengerLines = File.ReadAllLines(filePath);

            foreach (var item in passengerLines)
            {
                if (passengerLines.ElementAt(0) == item)
                {
                    continue;
                }
                var values = item.Split(','); // leeftijd, overleeft, classe, geslacht, country, 
                var siblings = values[7] != "" ? Convert.ToInt32(values[7]) : 0;
                var parents = values[8] != "" ? Convert.ToInt32(values[8]) : 0;

                passengerList.Add(new Passenger
                {
                    BoatClass = values[0],
                    Survived = values[1] != "" && Convert.ToBoolean(Convert.ToInt16(values[1])),
                    FirstName = values[3],
                    LastName = values[4],
                    Gender = values[5],
                    Age = values[6] != "" ? Convert.ToDouble(values[6]) : 0,
                    Country = values[13],
                    FamilyMembers = siblings + parents
                });
            }

            return passengerList;
        }
    }
}
