using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3_Data
{
    class Passenger
    {
        public string BoatClass { get; set; }
        public bool Survived { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public double? Age { get; set; }
        public string Country { get; set; }
        public int FamilyMembers { get; set; } 

    }
}
