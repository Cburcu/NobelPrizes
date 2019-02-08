using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HwNobelPrizes.Models
{
    public class PrizeList
    {
        public List<Prizes> Prizes { get; set; }
    }

    public class Prizes
    {
        public string Year { get; set; }
        public string Category { get; set; }
        public string OverAllMotivation { get; set; }
        public List<Laureates> Laureates { get; set; }
    }

    public class Laureates
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Motivation { get; set; }
        public string Share { get; set; }
    }
}

