using System.Text.Json.Serialization;

namespace Infraestructure.Dtos.Response
{
    public class GetStatsQueryResponse
    {
        [JsonPropertyName("count_mutant_dna")]
        public int CountMutantDna { get; set; }

        [JsonPropertyName("ratio")]
        public double Ratio { get; set; }

        [JsonPropertyName("count_human_dna")]
        public int CountHumanDna { get; set; }

    }
}
