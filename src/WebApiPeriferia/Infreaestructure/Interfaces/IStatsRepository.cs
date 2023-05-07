
using Infraestructure.Dtos.Response;

namespace Infraestructure.Interfaces
{
    public interface IStatsRepository
    {
        public void InsertStat(string dnaType);
        public Task<GetStatsQueryResponse> GetStats();
    }
}