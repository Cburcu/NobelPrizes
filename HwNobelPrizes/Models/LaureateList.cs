using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HwNobelPrizes.Models
{
    public class LaureateList
    {
        public List<LaureateData> Laureates { get; set; }

    }

    public class LaureateData
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public string BornCountry { get; set; }
        public string BornCountryCode { get; set; }
        public string BornCity { get; set; }
        public string DiedCountry { get; set; }
        public string DiedCountryCode { get; set; }
        public string DiedCity { get; set; }
        public string Gender { get; set; }
        public List<PrizeData> Prizes { get; set; }
    }

    public class PrizeData
    {
        public string Year { get; set; }
        public string Category { get; set; }
        public string Share { get; set; }
        public string Motivation { get; set; }
        public List<Affiliation> Affiliations { get; set; }
        //public string OverallMotivation { get; set; } --- bazılarında var
    }

    public class Affiliation
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
