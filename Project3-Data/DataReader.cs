using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project3_Data
{
    class DataReader
    {
        public List<Passenger> CreatePassengerList()
        {
            var passengerList = new List<Passenger>();

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName;
            var filePath = projectPath + "\\resource\\titanic.csv";
            
            var passengerLines = File.ReadAllLines(filePath);
            
            foreach (var item in passengerLines)
            {
                if (passengerLines.ElementAt(0) == item)
                {
                    continue;
                }
                var values = item.Split(',');
                passengerList.Add(new Passenger
                {
                    BoatClass = values[0],
                    Survived = values[1] != "" && Convert.ToBoolean(Convert.ToInt16(values[1])),
                    Name = values[2],
                    Gender = values[3],
                    Age = values[4] != "" ? Convert.ToDouble(values[4]) : 0,
                    Sibsp = values[5],
                    Parch = values[6],
                    Ticket = values[7],
                    Fare = values[8],
                    Cabin = values[9],
                    Embarked = values[10],
                    Boat = values[11],
                    Body = values[12],
                    Home = values[13]
                });
            }

            return passengerList;
        }
    }
}
