using Infraestructure.Dtos.Response;
using Infraestructure.Implementations;
using Infraestructure.Interfaces;
using Infraestructure.Settings;
using Microsoft.Extensions.Options;

namespace Infraestructure.Repository
{
    public class StatsRepository : SqlBase, IStatsRepository
    {
        IOptions<InfraestructureSettings> _settings;
        public StatsRepository(IOptions<InfraestructureSettings> settings) : base(settings.Value.SqlSettings.ConnectionString)
        {
            _settings = settings;
        }

        public async Task<GetStatsQueryResponse> GetStats()
        {
            GetStatsQueryResponse response = new();
            try
            {
                using (var rdr = await ExecuteSpResults(_settings.Value.SqlSettings.SpGetStats))
                {
                    for (int i = 0; i < rdr.Tables[0].Rows.Count; i++)
                    {
                        response = new GetStatsQueryResponse
                        {
                            CountHumanDna = Convert.ToInt32(rdr.Tables[0].Rows[i]["count_human_dna"]),
                            CountMutantDna = Convert.ToInt32(rdr.Tables[0].Rows[i]["count_mutant_dna"]),
                        };
                        response.Ratio = response.CountHumanDna > 0 ? (Convert.ToDouble(response.CountMutantDna) / Convert.ToDouble(response.CountHumanDna)) : 0;
                    }

                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void InsertStat(string dnaType)
        {
            string query = "";
            try
            {
                query = $"INSERT INTO [dbo].[tblStats] ([DnaType]) VALUES ('{dnaType}')";
                await ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
