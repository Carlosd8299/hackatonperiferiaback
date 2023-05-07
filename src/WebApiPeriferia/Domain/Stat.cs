using System.Text.Json.Serialization;

namespace Domain
{
    public class Stat
    { 
        public int CountMutantDna { get; set; }    
        public double Ratio { get; set; }
        public int CountHumanDna { get; set; }
    }
}