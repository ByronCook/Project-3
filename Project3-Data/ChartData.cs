namespace Project3_Data
{
    class ChartData
    {
        public int Survived { get; set; } = 0;
        public int Dead { get; set; } = 0;
        public int Female { get; set; } = 0;
        public int Male { get; set; } = 0;
        public int AgeUnder30 { get; set; } = 0;
        public int AgeOver30 { get; set; } = 0;
        public int AgeUnknown { get; set; } = 0;
        public int MaleSurivalRate { get; set; } = 0;
        public int FemalesAndKidsSurvivalRate { get; set; } = 0;
        public int SurvivedMalesFromCountry { get; set; } = 0;
        public int SurvivedFemalesFromCountry { get; set; } = 0;
        public int TitanicPassengers { get; set; } = 0;
        public int LusitaniaPassengers { get; set; } = 0;
    }
}
