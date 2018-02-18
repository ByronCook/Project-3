using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace Project3_Data
{
    class DataSorter
    {
        public List<ChartData> GetSurvivedData(List<Passenger> dataList)
        {
            var survivedTitanicPassengers = dataList.Count(t => t.Survived);
            var deadTitanicPassengers = dataList.Count(t => !t.Survived);

            return new List<ChartData>
            {
                new ChartData {Survived = survivedTitanicPassengers, Dead = deadTitanicPassengers, Female = 10}
                // new ChartData(); //2nd boat
            }; 
        }

        public List<ChartData> GetAgeData(List<Passenger> dataList)
        {

            var under30 = dataList.Count(e => e.Age < 30 && e.Age > 0);
            var over30 = dataList.Count(t => t.Age >= 30);
            var ageUnknown = dataList.Count(y => y.Age == 0);

            return new List<ChartData>
            {
                new ChartData { AgeUnder30 = under30, AgeOver30 = over30, AgeUnknown = ageUnknown}
            };
        }

        public void GetSurvivalRate(List<Passenger> dataList)
        {
            var males = dataList.Where(e => e.Gender == "Male" || e.Gender == "male");
            var females = dataList.Count(e => e.Gender == "Female" || e.Gender == "female");
            var maleKids = dataList.Where(e => e.Age < 16 && e.Age > 0 && (e.Gender == "male" || e.Gender == "Male" || string.IsNullOrEmpty(e.Gender)));

            var unknownGender = dataList.Count(e => string.IsNullOrEmpty(e.Gender));

            var totalfemales2= dataList.Count(e => (e.Gender == "Female" || e.Gender == "female") && e.Age > 16);
            var totalkids2 = dataList.Count(e => e.Age < 16 && e.Age >0);
            var survivedFemales = dataList.Count(e => (e.Gender == "Female" || e.Gender == "female") && e.Age > 16 && e.Survived);
            var survivedKids = dataList.Count(e => e.Age < 16 && e.Age > 0 && e.Survived);
            var deadFemales = totalfemales2 - survivedFemales;
            var deadKids = totalkids2 - survivedKids;

            var adultMales = males.Count() - maleKids.Count();
            var survivedMales = males.Count(e => e.Survived);

            var femalesurivalrate = (survivedFemales * 100) / totalfemales2;
            var kidsSurvivalrate = (survivedKids*100)/totalkids2;
            var maleSurvivalrate = (survivedMales*100)/adultMales;

            var femalesAndKids = totalfemales2 + totalkids2;

            return;
        }
    }
}
