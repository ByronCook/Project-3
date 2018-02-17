using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project3_Data
{
    class DataReader
    {
        public List<Passenger> CreatePassengerList()
        {
            var passengerList = new List<Passenger>();
          //  const string url = "https://docs.google.com/spreadsheets/d/1cNISQ0VuUx-9yTPsPnfM35px0LrPm1zrP4tgZxkZ2Bs/pub?output=tsv";
            var passengerLines =
                File.ReadAllLines(
                    "C:\\Users\\Administrator\\Documents\\Visual Studio 2015\\Projects\\Project3-Data\\Project3-Data\\resource\\titanic.csv");


            foreach (var item in passengerLines)
            {
                if (passengerLines.ElementAt(0) == item)
                {
                    continue;
                }
                var values = item.Split(',');
                passengerList.Add(new Passenger()
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


            //var rowResult = fileList.Split('\r');

            //if (rowResult.Count() < 1)
            //{
            //    return artistsList;
            //}

            //foreach (var item in rowResult)
            //{
            //    if (rowResult.ElementAt(0) == item)
            //    {
            //        continue;
            //    }
            //    var artistData = item.Replace("\n", "").Split('\t');
            //    artistsList.Add(new Passenger
            //    {
            //        Name = artistData[0],
            //        SetStartTime = Convert.ToDateTime(artistData[1]),
            //        SetEndTime = Convert.ToDateTime(artistData[2]),
            //        Stage = artistData[3],
            //        Day = Convert.ToInt32(artistData[4]),
            //        EventName = artistData[5]
            //    });
            //}
            //return artistsList;
        }
    }
}
